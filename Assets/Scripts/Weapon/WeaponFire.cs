using System.Collections;
using UnityEngine;
using VContainer;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] private float _swapDelay;
    [SerializeField] private SoundDataSO _sfx;

    private PlayerAnimation _animation;
    private PlayerController _player;
    private PlayerWeaponRigger _rigger;
    private WeaponInputModel _inputModel;
    private WeaponController _controller;
    private WeaponSlot _slot;
    private Coroutine _fireRoutine;
    private Coroutine _reloadRoutine;
    private WaitForSeconds _waitForSwap;
    private IPauseService _pause;
    [Inject] private ISoundService _soundService;
    private IWeapon _currentWeapon => _slot.CurrentWeapon;

    private float _elapsed;
    private bool _onSwap => _inputModel.OnSwap.Value;
    private bool _onReload => _reloadRoutine != null;
    private bool _isCoolOn => _elapsed >= _currentWeapon.Model.CoolDown;
    private bool _isEmpty => _currentWeapon.Model.CurrentMagazine.Value <= 0;
    private bool _isPaused => _pause.IsPaused;
    private bool _canFire => _isCoolOn && !_isEmpty && !_onSwap && !_onReload && !_isPaused;

    private void OnEnable() => BindEvents();
    private void Start() => Init();
    private void Update() => RefreshTime();
    private void OnDisable() => UnBindEvents();

    [Inject]
    public void Inject(WeaponInputModel inputModel, WeaponController controller, PlayerAnimation animation, PlayerWeaponRigger rigger, IPauseService pause, PlayerController player)
    {
        _player = player;
        _pause = pause;
        _rigger = rigger;
        _animation = animation;
        _inputModel = inputModel;
        _controller = controller;
        _rigger.ActiveWeapon(_slot.CurrentWeapon.Model.Prefab);
        _rigger.ApplyRig(_slot.CurrentWeapon.Model.Prefab);
    }

    private void CheckFire(bool onFire)
    {
        if (_player.HasBeacon)
        {
            if (onFire) _player.ThrowBeacon();
            return;
        }

        if (onFire) StartFire();
        else StopFire();
    }
    private void StartFire()
    {
        if (_fireRoutine != null) return;
        _fireRoutine = StartCoroutine(FireRoutine());
    }
    private void StopFire()
    {
        if (_fireRoutine == null) return;
        StopCoroutine(_fireRoutine);
        _fireRoutine = null;
        _animation.Fire(false);
    }
    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if (_canFire)
            {
                _currentWeapon.Fire(_controller);
                _currentWeapon.Model.CurrentMagazine.Value--;
                _animation.Fire(true);
                _elapsed = 0.0f;
            }

            else if (_isEmpty)
            {
                _fireRoutine = null;
                _animation.Fire(false);
                yield break;
            }

            yield return null;
        }
    }

    private void CheckReload(bool onReload)
    {
        if (onReload)
        {
            if (_currentWeapon.Model.AmmoClip.Value <= 0 || _currentWeapon.Model.CurrentMagazine.Value >= _currentWeapon.Model.MaxMagazine.Value)
            {
                _inputModel.IsReload.Value = false;
                return;
            }

            _soundService.PlaySFX(_sfx);
            StartReload();
        }
        else
        {
            StopReload();
        }
    }

    private void StartReload()
    {
        if (_reloadRoutine != null) return;
        _animation.Reload(true);
        _currentWeapon.Model.CurrentReload.Value = 0.0f;
        _reloadRoutine = StartCoroutine(ReloadRoutine());
    }

    private void StopReload()
    {
        if (_reloadRoutine == null) return;

        _animation.Reload(false);
        StopCoroutine(_reloadRoutine);
        _reloadRoutine = null;

        _currentWeapon.Model.CurrentReload.Value = 0f;

        if (_inputModel.IsFired.Value)
            StartFire();
    }

    private IEnumerator ReloadRoutine()
    {
        while (_currentWeapon.Model.CurrentReload.Value < _currentWeapon.Model.ReloadTime)
        {
            if (_onSwap)
            {
                _currentWeapon.Model.CurrentReload.Value = 0f;
                _reloadRoutine = null;
                _animation.Reload(false);
                _inputModel.IsReload.Value = false;

                if (_inputModel.IsFired.Value)
                    StartFire();

                yield break;
            }

            _currentWeapon.Model.CurrentReload.Value += Time.deltaTime;
            yield return null;
        }

        _currentWeapon.Model.CurrentReload.Value = 0.0f;
        _currentWeapon.Model.CurrentMagazine.Value = _currentWeapon.Model.MaxMagazine.Value;

        _currentWeapon.Model.AmmoClip.Value--;

        _reloadRoutine = null;
        _animation.Reload(false);

        _inputModel.IsReload.Value = false;

        if (_inputModel.IsFired.Value) StartFire();
    }

    private void RefreshTime()
    {
        _elapsed += Time.deltaTime;
    }

    private void CheckSwap(float value)
    {
        if (_player.HasBeacon) return;
        if (_onSwap) return;
        if (value > 0) StartCoroutine(SwapRoutine(true));
        else if (value < 0) StartCoroutine(SwapRoutine(false));
    }

    private IEnumerator SwapRoutine(bool next)
    {
        _inputModel.OnSwap.Value = true;
        _animation.Swap(true);
        _rigger.DeactiveWeapon(_currentWeapon.Model.Prefab);
        _rigger.DisApplyRig();
        yield return _waitForSwap;

        if (next) _slot.Next();
        else _slot.Prev();

        _rigger.ActiveWeapon(_currentWeapon.Model.Prefab);
        _rigger.ApplyRig(_currentWeapon.Model.Prefab);
        _animation.Swap(false);
        _inputModel.OnSwap.Value = false;
    }

    public void SetWeapon(WeaponSlot slot)
    {
        _slot = slot;
        _elapsed = 0.0f;
    }

    private void Init()
    {
        _waitForSwap = new WaitForSeconds(_swapDelay);
    }

    private void BindEvents()
    {
        _inputModel.SwapValue.AddListener(CheckSwap);
        _inputModel.IsFired.AddListener(CheckFire);
        _inputModel.IsReload.AddListener(CheckReload);
    }
    private void UnBindEvents()
    {
        _inputModel.SwapValue.RemoveListener(CheckSwap);
        _inputModel.IsFired.RemoveListener(CheckFire);
        _inputModel.IsReload.RemoveListener(CheckReload);
    }
}
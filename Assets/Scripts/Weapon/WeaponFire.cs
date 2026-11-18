using System.Collections;
using UnityEngine;
using VContainer;

public class WeaponFire : MonoBehaviour
{
    private WeaponInputModel _inputModel;
    private WeaponSlot _slot;
    private Coroutine _fireRoutine;
    private Coroutine _reloadRoutine;
    private WaitForSeconds _waitForSwap;
    private IWeapon _currentWeapon => _slot.CurrentWeapon;

    [SerializeField] private float _swapDelay;
    private float _elapsed;
    private bool _isEmpty => _currentWeapon.Model.CurrentMagazine.Value <= 0;
    private bool _onSwap => _inputModel.OnSwap.Value;
    private bool _onReload => _inputModel.IsReload.Value;
    private bool _isCoolOn => _elapsed >= _currentWeapon.Model.CoolDown;
    private bool _canFire => _isCoolOn && !_isEmpty && !_onSwap && !_onReload;

    private void OnEnable() => BindEvents();
    private void Start() => Init();
    private void Update() => RefreshTime();
    private void OnDisable() => UnBindEvents();

    [Inject]
    public void Inject(WeaponInputModel inputModel)
    {
        _inputModel = inputModel;
        _inputModel.CurrentWeapon.Value = _slot.CurrentWeapon.Model.AnimIndex;
    }

    private void CheckFire(bool onFire)
    {
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
    }
    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if (_canFire)
            {
                _currentWeapon.Fire();
                _elapsed = 0.0f;
            }
            yield return null;
        }
    }

    private void CheckReload(bool onReload)
    {
        if (_currentWeapon.Model.AmmoClip <= 0)
        {
            _inputModel.IsReload.Value = false;
            return;
        }
        if (onReload) StartReload();
        else StopReload();
    }
    private void StartReload()
    {
        if (_reloadRoutine != null) return;
        _currentWeapon.Model.CurrentReload.Value = 0.0f;
        _reloadRoutine = StartCoroutine(ReloadRoutine());
    }
    private void StopReload()
    {
        if (_reloadRoutine == null) return;
        StopCoroutine(_reloadRoutine);
        _reloadRoutine = null;
    }
    private IEnumerator ReloadRoutine()
    {
        while (true)
        {
            _currentWeapon.Model.CurrentReload.Value += Time.deltaTime;
            if (_currentWeapon.Model.CurrentReload.Value >= _currentWeapon.Model.ReloadTime)
            {
                _currentWeapon.Model.CurrentReload.Value = 0.0f;
                _currentWeapon.Model.CurrentMagazine.Value = _currentWeapon.Model.MaxMagazine.Value;
                _inputModel.IsReload.Value = false;
            }

            if (_onSwap)
            {
                _inputModel.IsReload.Value = false;
                _currentWeapon.Model.CurrentReload.Value = 0.0f;
            }

            yield return null;
        }
    }
    private void RefreshTime()
    {
        _elapsed += Time.deltaTime;
    }

    private void CheckSwap(float value)
    {
        if (_inputModel.OnSwap.Value) return;
        if (value != 0) StartCoroutine(SwapRoutine());
    }

    private IEnumerator SwapRoutine()
    {
        _inputModel.OnSwap.Value = true;
        yield return _waitForSwap;
        _inputModel.CurrentWeapon.Value = _slot.CurrentWeapon.Model.AnimIndex;
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
        _inputModel.IsReload.AddListener(CheckReload);
    }
}
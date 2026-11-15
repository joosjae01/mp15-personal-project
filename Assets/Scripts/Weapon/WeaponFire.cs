using System.Collections;
using UnityEngine;
using VContainer;

public class WeaponFire : MonoBehaviour
{
    [Inject] private WeaponInputModel _inputModel;
    private WeaponSlot _slot;
    private Coroutine _fireRoutine;
    private IWeapon _currentWeapon => _slot.CurrentWeapon;

    private float _elapsed;
    private bool _isEmpty => _currentWeapon.Model.CurrentMagazine.Value <= 0;
    private bool _isCoolOn => _elapsed >= _currentWeapon.Model.CoolDown;
    private bool _canFire => _isCoolOn && !_isEmpty;

    private void OnEnable() => BindEvents();
    private void Update() => RefreshTime();
    private void OnDisable() => UnBindEvents();

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

    private void RefreshTime()
    {
        _elapsed += Time.deltaTime;
    }

    private void CheckSwap(float value)
    {
        if(value != 0) _elapsed = 0.0f;
    }

    public void SetWeapon(WeaponSlot slot)
    {
        _slot = slot;
        _elapsed = 0.0f;
    }

    private void BindEvents()
    {
        _inputModel.SwapValue.AddListener(CheckSwap);
        _inputModel.IsFired.AddListener(CheckFire);
    }
    private void UnBindEvents()
    {
        _inputModel.SwapValue.RemoveListener(CheckSwap);
        _inputModel.IsFired.RemoveListener(CheckFire);
    }
}
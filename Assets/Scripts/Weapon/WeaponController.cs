using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class WeaponController : MonoBehaviour
{
    private WeaponInputModel _inputModel;
    private IWeaponFactory _factory;
    private List<IWeapon> _weaponSlot = new();
    private float _elapsed;
    private int _index;

    private bool _isEmpty => _weaponSlot[_index].Model.CurrentMagazine.Value <= 0;
    private bool _canFire => _elapsed >= _weaponSlot[_index].Model.CoolDown && !_isEmpty;
    private Coroutine _fireRoutine;

    private void OnEnable() => BindEvents();
    private void Update() => _elapsed += Time.deltaTime;
    private void OnDisable() => UnBindEvents();

    [Inject]
    public void Inject(WeaponInputModel inputModel, WeaponSlotSO dataSO, IWeaponFactory weaponFactory)
    {
        _factory = weaponFactory;
        _inputModel = inputModel;
        foreach (WeaponContext context in dataSO.WeaponContext) GetWeapon(context);
    }

    private void GetWeapon(WeaponContext context)
    {
        IWeapon temp = _factory.Create(context);
        _weaponSlot.Add(temp);
        Debug.Log($"{name} : {temp} 생성");
        Debug.Log($"{name} : 공격 속도 {temp.Model.FireRate}");
    }

    private void CheckFire(bool onFire)
    {
        if (onFire) StartFire();
        else StopFire();
    }

    private void StartFire()
    {
        if (_fireRoutine != null) return;
        Debug.Log($"{name} : 사격 시작");
        _fireRoutine = StartCoroutine(FireRoutine());
    }

    private void StopFire()
    {
        if (_fireRoutine == null) return;
        Debug.Log($"{name} : 사격 종료");
        StopCoroutine(_fireRoutine);
        _fireRoutine = null;
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if (_canFire)
            {
                _weaponSlot[_index].Fire();
                _elapsed = 0.0f;
            }
            yield return null;
        }
    }

    private void Swap(float value)
    {
        if (value > 0)
        {
            _index++;
            if (_index >= _weaponSlot.Count) _index = 0;
        }

        else if (value < 0)
        {
            _index--;
            if (_index <= -1) _index = _weaponSlot.Count - 1;
        }

        Debug.Log($"{name} : {_weaponSlot[_index]}로 변경");
        _elapsed = 0.0f;
    }

    private void BindEvents()
    {
        _inputModel.IsFired.AddListener(CheckFire);
        _inputModel.SwapValue.AddListener(Swap);
    }

    private void UnBindEvents()
    {
        _inputModel.IsFired.RemoveListener(CheckFire);
        _inputModel.SwapValue.RemoveListener(Swap);
    }
}

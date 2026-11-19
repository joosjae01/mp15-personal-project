using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponFire _weaponFire;
    [SerializeField] private Transform _muzzlePoint;

    private WeaponSlot _slot;

    [Inject]
    public void Inject(WeaponSlotSO dataSO, WeaponInputModel inputModel, IWeaponFactory factory)
    {

        List<IWeapon> slot = new();
        foreach (WeaponContext context in dataSO.WeaponContext)
        {
            IWeapon weapon = factory.Create(context);
            slot.Add(weapon);
        }

        _slot = new(slot);
        _weaponFire.SetWeapon(_slot);
    }

    public WeaponSlot GetWeaponSlot()
    {
        return _slot;
    }

    public Transform GetMuzzlePoint()
    {
        return _muzzlePoint;
    }
}

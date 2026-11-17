using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponFire _weaponFire;

    private WeaponSlot _slot;
    private WeaponSwap _swap;

    private void OnEnable() => _swap.BindEvents();
    private void OnDisable() => _swap.UnBindEvents();

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
        _swap = new(inputModel, _slot);
        _weaponFire.SetWeapon(_slot);
    }

    public WeaponSlot GetWeaponSlot()
    {
        return _slot;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot
{
    public IReadOnlyList<IWeapon> Slot { get; private set; }
    public IWeapon CurrentWeapon { get => Slot[_index]; }
    private int _index;

    public WeaponSlot(List<IWeapon> weaponSlot) => Slot = weaponSlot;
    public void Prev()
    {
        if(_index > 0) _index--;
        else _index = Slot.Count - 1;
    }

    public void Next()
    {
        if (_index == Slot.Count - 1) _index = 0;
        else _index++;
    }
}

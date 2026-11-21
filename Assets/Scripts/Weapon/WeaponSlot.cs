using System;
using System.Collections.Generic;

public class WeaponSlot : IDisposable
{
    public List<IWeapon> Slot { get; private set; }
    public IWeapon CurrentWeapon { get => Slot[_index]; }
    private event Action<IWeapon> _onWeaponChanged;
    private int _index;

    public WeaponSlot(List<IWeapon> weaponSlot)
    {
        Slot = weaponSlot;
    }

    public void Prev()
    {
        if(_index > 0) _index--;
        else _index = Slot.Count - 1;
        _onWeaponChanged?.Invoke(CurrentWeapon);
    }

    public void Next()
    {
        if (_index == Slot.Count - 1) _index = 0;
        else _index++;
        _onWeaponChanged?.Invoke(CurrentWeapon);
    }

    public int GetIndex() => _index;
    public void AddListener(Action<IWeapon> action) => _onWeaponChanged += action;
    public void RemoveListener(Action<IWeapon> action) => _onWeaponChanged -= action;
    public void RemoveAllListener()
    {
        _onWeaponChanged = null;
    }

    public void Dispose()
    {
        RemoveAllListener();
    }
}

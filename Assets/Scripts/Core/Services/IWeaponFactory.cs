using UnityEngine;

public interface IWeaponFactory
{
    public IWeapon Create(WeaponContext context);
}

using UnityEngine;

public class MockWeapon : IWeapon
{
    public WeaponDataModel Model { get; }
    public void Fire()
    {
        Debug.Log($"MockWeapon : 발사");
    }
}

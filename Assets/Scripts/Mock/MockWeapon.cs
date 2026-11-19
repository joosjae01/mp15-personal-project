using UnityEngine;

public class MockWeapon : IWeapon
{
    public WeaponDataModel Model { get; }
    public void Fire(WeaponController controller)
    {
        Debug.Log($"MockWeapon : 발사");
    }
}

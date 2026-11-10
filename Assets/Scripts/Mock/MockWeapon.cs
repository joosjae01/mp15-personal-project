using UnityEngine;

public class MockWeapon : IWeapon
{
    public void Fire()
    {
        Debug.Log($"MockWeapon : 발사");
    }
}

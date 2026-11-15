using UnityEngine;

public class ShotGun : IWeapon
{
    private WeaponDataModel _model;
    public WeaponDataModel Model { get => _model; }

    public ShotGun(WeaponContext context) => _model = new WeaponDataModel(context.DataSO);

    public void Fire() => Debug.Log("ShotGun : Fired");
}

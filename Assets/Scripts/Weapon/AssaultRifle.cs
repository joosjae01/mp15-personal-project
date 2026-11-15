using UnityEngine;

public class AssaultRifle : IWeapon
{
    private WeaponDataModel _model;
    public WeaponDataModel Model { get => _model; }

    public AssaultRifle(WeaponContext context) => _model = new WeaponDataModel(context.DataSO);

    public void Fire() => Debug.Log("Assualt Rifle : Fired");
}

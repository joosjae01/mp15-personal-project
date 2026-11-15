using UnityEngine;

public class Pistol : IWeapon
{
    private WeaponDataModel _model;
    public WeaponDataModel Model { get => _model; }

    public Pistol(WeaponContext context) => _model = new WeaponDataModel(context.DataSO);

    public void Fire() => Debug.Log("Pistol : Fire");
}

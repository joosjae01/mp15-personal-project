using UnityEngine;

public class ShotGun : IWeapon
{
    private WeaponDataModel _model;
    private ISoundService _soundService;
    public WeaponDataModel Model { get => _model; }

    public ShotGun(WeaponContext context, ISoundService soundService)
    {
        _soundService = soundService;
        _model = new WeaponDataModel(context.DataSO);
    }

    public void Fire()
    {
        _soundService.PlaySFX(_model.SFX);
    }
}

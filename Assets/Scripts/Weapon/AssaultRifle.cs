using UnityEngine;

public class AssaultRifle : IWeapon
{
    private WeaponDataModel _model;
    private ISoundService _soundService;
    public WeaponDataModel Model { get => _model; }

    public AssaultRifle(WeaponContext context, ISoundService soundService)
    {
        _soundService = soundService;
        _model = new WeaponDataModel(context.DataSO);
    }

    public void Fire(WeaponController controller)
    {
        _soundService.PlaySFX(_model.SFX);
    }
}

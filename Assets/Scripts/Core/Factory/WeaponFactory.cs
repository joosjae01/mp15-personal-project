using VContainer;

public class WeaponFactory : IWeaponFactory
{
    [Inject] private ISoundService _soundService;
    [Inject] private IPoolService _poolService;
    [Inject] private PlayerWeaponRigger _rigger;

    public IWeapon Create(WeaponContext context)
    {
        IWeapon temp = context.Type switch {
            WeaponType.Pistol => new Pistol(context, _soundService, _poolService),
            WeaponType.AssaultRifle => new AssaultRifle(context, _soundService, _poolService),
            WeaponType.ShotGun => new ShotGun(context, _soundService),
            _ => null
        };

        _rigger.CreateWeapon(temp.Model.Prefab);
        return temp;
    }
}

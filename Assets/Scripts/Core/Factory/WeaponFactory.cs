using VContainer;

public class WeaponFactory : IWeaponFactory
{
    [Inject] private ISoundService _soundService;
    [Inject] private IPoolService _poolService;

    public IWeapon Create(WeaponContext context)
    {
        IWeapon temp = context.Type switch {
            WeaponType.Pistol => new Pistol(context, _soundService, _poolService),
            WeaponType.AssaultRifle => new AssaultRifle(context, _soundService, _poolService),
            WeaponType.ShotGun => new ShotGun(context, _soundService),
            _ => null
        };

        return temp;
    }
}

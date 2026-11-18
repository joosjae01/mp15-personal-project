using VContainer;

public class WeaponFactory : IWeaponFactory
{
    [Inject] private ISoundService _soundService;

    public IWeapon Create(WeaponContext context)
    {
        IWeapon temp = context.Type switch {
            WeaponType.Pistol => new Pistol(context, _soundService),
            WeaponType.AssaultRifle => new AssaultRifle(context, _soundService),
            WeaponType.ShotGun => new ShotGun(context, _soundService),
            _ => null
        };

        return temp;
    }
}

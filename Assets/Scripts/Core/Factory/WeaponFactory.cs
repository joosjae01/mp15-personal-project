public class WeaponFactory : IWeaponFactory
{
    public IWeapon Create(WeaponContext context)
    {
        IWeapon temp = context.Type switch {
            WeaponType.Pistol => new Pistol(context),
            WeaponType.AssaultRifle => new AssaultRifle(context),
            WeaponType.ShotGun => new ShotGun(context),
            _ => null
        };

        return temp;
    }
}

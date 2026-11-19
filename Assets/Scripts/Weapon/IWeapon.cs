public interface IWeapon
{
    public WeaponDataModel Model { get; }
    public void Fire(WeaponController controller);
}

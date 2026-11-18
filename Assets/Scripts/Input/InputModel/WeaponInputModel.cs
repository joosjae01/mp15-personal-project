public class WeaponInputModel
{
    public ObservableProperty<float> SwapValue;
    public ObservableProperty<bool> IsFired;
    public ObservableProperty<bool> IsReload;
    public ObservableProperty<bool> OnSwap;
    public ObservableProperty<int> CurrentWeapon;

    public WeaponInputModel()
    {
        CurrentWeapon = new(-1);
        SwapValue = new(0.0f);
        IsFired = new(false);
        IsReload = new(false);
        OnSwap = new(false);
    }
}

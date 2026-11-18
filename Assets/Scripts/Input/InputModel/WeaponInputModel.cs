public class WeaponInputModel
{
    public ObservableProperty<float> SwapValue;
    public ObservableProperty<bool> IsFired;
    public ObservableProperty<bool> IsReload;
    public ObservableProperty<bool> OnSwap;

    public WeaponInputModel()
    {
        SwapValue = new(0.0f);
        IsFired = new(false);
        IsReload = new(false);
        OnSwap = new(false);
    }
}

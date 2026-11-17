public class WeaponInputModel
{
    public ObservableProperty<float> SwapValue;
    public ObservableProperty<bool> IsFired;
    public ObservableProperty<bool> IsReload;

    public WeaponInputModel()
    {
        SwapValue = new(0.0f);
        IsFired = new(false);
        IsReload = new(false);
    }
}

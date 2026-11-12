using UnityEngine;

public class WeaponInputModel
{
    public ObservableProperty<float> SwapValue;
    public ObservableProperty<bool> IsFired;
    public ObservableProperty<bool> IsReload;

    public WeaponInputModel()
    {
        IsFired = new(false);
        IsReload = new(false);
    }
}

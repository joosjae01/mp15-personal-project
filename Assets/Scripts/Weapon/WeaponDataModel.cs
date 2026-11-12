public class WeaponDataModel
{
    public ObservableProperty<int> CurrentMagazine = new(0);
    public ObservableProperty<int> MaxMagazine = new(0);
    public float Power { get; private set; }
    public float FireRate { get; private set; }
    public float Stability { get; private set; }
    public float Range { get; private set; }

    public WeaponDataModel(WeaponData dataSO)
    {
        MaxMagazine.Value = dataSO.MaxMagazine;
        CurrentMagazine.Value = dataSO.MaxMagazine;
        Power = dataSO.Power;
        FireRate = dataSO.FireRate;
        Stability = dataSO.Stability;
        Range = dataSO.Range;
    }
}

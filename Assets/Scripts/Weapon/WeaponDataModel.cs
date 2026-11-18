public class WeaponDataModel
{
    public ObservableProperty<int> CurrentMagazine;
    public ObservableProperty<int> MaxMagazine;
    public ObservableProperty<float> CurrentReload;
    public SoundDataSO SFX {get; private set;}
    public int AnimIndex { get; private set; }
    public float Power { get; private set; }
    public float CoolDown { get; private set; }
    public float FireRate { get; private set; }
    public float Stability { get; private set; }
    public float Range { get; private set; }
    public int AmmoClip { get; private set; }
    public float ReloadTime { get; private set; }
    

    public WeaponDataModel(WeaponDataSO dataSO)
    {
        MaxMagazine = new(dataSO.MaxMagazine);
        CurrentMagazine = new(dataSO.MaxMagazine);
        CurrentReload = new(0.0f);
        Power = dataSO.Power;
        FireRate = dataSO.FireRate;
        Stability = dataSO.Stability;
        Range = dataSO.Range;
        CoolDown = 1 / FireRate;
        SFX = dataSO.SFX;
        AmmoClip = dataSO.AmmoClip;
        ReloadTime = dataSO.ReloadTime;
        AnimIndex = dataSO.AnimationIndex;
    }
}

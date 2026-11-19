using UnityEngine;

public class WeaponDataModel
{
    public ObservableProperty<int> CurrentMagazine;
    public ObservableProperty<int> MaxMagazine;
    public ObservableProperty<float> CurrentReload;
    public SoundDataSO SFX {get; private set;}
    public GameObject VFX { get; private set; }
    public GameObject Prefab { get; private set; }

    public float ReloadTime { get; private set; }
    public float Stability { get; private set; }
    public float CoolDown { get; private set; }
    public float FireRate { get; private set; }
    public float Power { get; private set; }
    public float Range { get; private set; }

    public int AnimIndex { get; private set; }
    public int AmmoClip { get; private set; }
    
    

    public WeaponDataModel(WeaponDataSO dataSO)
    {
        CurrentMagazine = new(dataSO.MaxMagazine);
        CurrentReload = new(0.0f);
        MaxMagazine = new(dataSO.MaxMagazine);
        SFX = dataSO.SFX;
        VFX = dataSO.VFX;
        Prefab = dataSO.Prefab;

        ReloadTime = dataSO.ReloadTime;
        Stability = dataSO.Stability;
        FireRate = dataSO.FireRate;
        CoolDown = 1 / FireRate;
        Power = dataSO.Power;
        Range = dataSO.Range;

        AnimIndex = dataSO.AnimationIndex;
        AmmoClip = dataSO.AmmoClip;
    }
}

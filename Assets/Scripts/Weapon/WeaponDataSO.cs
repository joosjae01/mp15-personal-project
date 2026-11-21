using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public GameObject VFX;
    public SoundDataSO SFX;
    public Sprite Icon;
    public float Power;
    public float FireRate;
    public float Stability;
    public int MaxMagazine;
    public int AmmoClip;
    public float Range;
    public float ReloadTime;
}

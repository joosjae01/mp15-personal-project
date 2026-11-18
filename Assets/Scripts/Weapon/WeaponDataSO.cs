using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string Name;
    public List<AnimContext> AnimationList;
    public GameObject Prefab;
    public SoundDataSO SFX;
    public Sprite Icon;
    public float Power;
    public float FireRate;
    public float Stability;
    public int MaxMagazine;
    public float Range;
}

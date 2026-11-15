using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public Sprite Icon;
    public AudioClip SoundEffect;
    public List<AnimContext> AnimationList;
    public float Power;
    public float FireRate;
    public float Stability;
    public int MaxMagazine;
    public float Range;
}

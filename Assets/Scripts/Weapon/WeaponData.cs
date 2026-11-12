using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject Prefab;
    public Sprite Icon;
    public AudioClip SoundEffect;
    public float Power;
    public float FireRate;
    public float Stability;
    public int MaxMagazine;
    public float Range;
}

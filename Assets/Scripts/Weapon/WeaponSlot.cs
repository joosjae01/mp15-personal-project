using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSlot", menuName = "SO/Delivery/WeaponSlot")]
public class WeaponSlot : ScriptableObject
{
    public List<IWeapon> WeaponList;
}

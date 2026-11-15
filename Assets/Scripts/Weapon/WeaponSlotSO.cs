using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSlot", menuName = "SO/Delivery/WeaponSlot")]
public class WeaponSlotSO : ScriptableObject
{
    public List<WeaponContext> WeaponContext;
}

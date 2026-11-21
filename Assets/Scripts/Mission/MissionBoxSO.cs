using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MissionBox", menuName = "SO/Mission/MissionBox")]
public class MissionBoxSO : ScriptableObject
{
    [NonSerialized] public MissionDataSO Data;
    [NonSerialized] public List<TargetContext> TargetList;
    [NonSerialized] public List<WeaponContext> Weapons;
    [NonSerialized] public List<StratagemDataSO> Staratgems;
}

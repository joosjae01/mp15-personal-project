using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MissionData", menuName = "SO/Mission/MissionData")]
public class MissionDataSO : ScriptableObject
{
    public string Name;
    public string Desc;
    public List<TargetContext> TargetContextList;
}

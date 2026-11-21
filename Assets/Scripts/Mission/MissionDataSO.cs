using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MissionData", menuName = "SO/Mission/MissionData")]
public class MissionDataSO : ScriptableObject
{
    public List<TargetContext> TargetContextList;
    public TargetContext FinalTarget;


    public List<TargetContext> GenerateList()
    {
        List<TargetContext> table = new(TargetContextList);
        List<TargetContext> result = new();

        for(int i = 0; i < 2; i++)
        {
            int index = Random.Range(0, table.Count);
            result.Add(table[index]);
            table.RemoveAt(index);
        }

        result.Add(FinalTarget);
        return result;
    }
}

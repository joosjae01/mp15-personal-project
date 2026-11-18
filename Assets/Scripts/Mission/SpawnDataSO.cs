using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_SpawnData", menuName = "SO/Mission/SpawnData")]
public class SpawnDataSO : ScriptableObject
{
    public string Name;
    public List<EnemySquad> EnemySquadList;
    public int Weight;
}

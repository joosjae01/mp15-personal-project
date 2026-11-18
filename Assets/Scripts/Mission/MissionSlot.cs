using UnityEngine;

[CreateAssetMenu(fileName = "SO_MissionSlot", menuName = "SO/Mission/MissionSlot")]
public class MissionSlotSO : ScriptableObject
{
    public MissionDataSO DataSO { get; private set; }
}

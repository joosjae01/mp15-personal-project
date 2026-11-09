using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [field:SerializeField] public int MaxHealth { get; set; }
    [field:SerializeField] public int MoveSpeed { get; set; }
}

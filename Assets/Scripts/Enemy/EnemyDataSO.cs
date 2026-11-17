using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemyData", menuName = "SO/Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public int MaxHealth;
    public int Shield;
    public float MoveSpeed;
    public float AttackValue;
}

using UnityEngine;

public abstract class EnemyAttackBase : ScriptableObject
{
    public abstract void Attack(PlayerController player, EnemyController enemy);
}

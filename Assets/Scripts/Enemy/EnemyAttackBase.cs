using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    public abstract void Attack(PlayerController player, EnemyController enemy);
}

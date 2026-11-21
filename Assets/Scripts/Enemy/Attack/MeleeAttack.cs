using UnityEngine;

public class MeleeAttack : EnemyAttackBase
{
    [SerializeField] private float _boxWidth;
    [SerializeField] private LayerMask _playerMask;

    public override void Attack(PlayerController player, EnemyController enemy)
    {
        EnemyDataModel data = enemy.DataModel;

        float depth = data.AttackRange;
        Vector3 size = new Vector3(_boxWidth, _boxWidth, depth);
        Vector3 center = enemy.transform.position + enemy.transform.forward * (depth * 0.5f);

        if(Physics.CheckBox(center, size * 0.5f, enemy.transform.rotation, _playerMask))
        {
            player.Health.TakeDamage(data.AttackValue);
        }
    }
}

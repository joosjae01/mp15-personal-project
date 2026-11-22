using UnityEngine;
using VContainer;

public class RangedAttack : EnemyAttackBase
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _muzzlePoint;

    [Inject] private IPoolService _poolService;

    public override void Attack(PlayerController player, EnemyController enemy)
    {
        EnemyDataModel model = enemy.DataModel;

        Vector3 muzzle = _muzzlePoint.position;
        Vector3 target = player.transform.position + Vector3.up;
        Vector3 dir = (target - muzzle).normalized;

        IPoolable projectile = _poolService.Take(_prefab);
        projectile.GameObject.transform.position = muzzle;
        projectile.GameObject.GetComponent<EnemyProjectile>().Launch(player, dir, model.AttackValue);
    }
}

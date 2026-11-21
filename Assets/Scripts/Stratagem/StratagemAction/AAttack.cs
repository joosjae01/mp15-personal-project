using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "SO/Stratagem/StratagemAction/Attack")]
public class AAttack : StratagemActionSO
{
    public override void Enter(ActionContext context)
    {
        if (context.Thrown != null)
        {
            IPoolable poolable;
            context.Thrown.TryGetComponent<IPoolable>(out poolable);
            poolable.Source.Release(poolable);
        }

        if (context.Targets.Count == 0) return;

        foreach(EnemyController enemy in context.Targets)
        {
            enemy.TakeDamage(context.Damage);
        }

        context.Targets.Clear();
    }
}

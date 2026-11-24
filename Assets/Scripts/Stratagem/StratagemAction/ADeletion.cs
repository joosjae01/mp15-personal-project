using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Deletion", menuName = "SO/Stratagem/StratagemAction/Deletion")]
public sealed class ADeletion : StratagemActionSO
{
    public override void Enter(ActionContext context)
    {
        if (context.Thrown != null)
        {
            IPoolable poolable;
            context.Thrown.TryGetComponent<IPoolable>(out poolable);
            poolable.Source.Release(poolable);
            context.Thrown = null;
        }
    }
}

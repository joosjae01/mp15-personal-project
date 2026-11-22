using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Deletion", menuName = "SO/Stratagem/StratagemAction/Deletion")]
public sealed class ADeletion : StratagemActionSO
{
    public override void Enter(ActionContext context)
    {
        if (context.Creation != null)
        {
            Destroy(context.Creation.GameObject);
            context.Thrown = null;
        }
    }
}

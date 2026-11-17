using UnityEngine;
using UnityEngine.Events;

public abstract class StratagemActionSO : ScriptableObject, IStratagemAction
{
    public abstract void Enter(ActionContext context);
    public float Delay;
}

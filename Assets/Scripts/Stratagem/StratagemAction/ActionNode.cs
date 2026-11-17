using System;
using System.Collections;
using UnityEngine;

public class ActionNode
{
    public ActionContext Context { get; set; }
    private event Action<ActionContext> _onExited;
    private StratagemActionSO _actionSO;
    private WaitForSeconds _delay;

    public ActionNode(StratagemActionSO actionSO)
    {
        _actionSO = actionSO;
    }

    public void Enter(ActionContext context)
    {
        _actionSO.Enter(context);
        if(context.Caster != null) context.Caster.StartCoroutine(DelayRoutine());
        Exit(context);
    }

    private IEnumerator DelayRoutine()
    {
        if(_actionSO.Delay != 0)
        {
            _delay = new WaitForSeconds(_actionSO.Delay);
            yield return _delay;
        }

        yield return null;
    }

    public void Exit(ActionContext context) => _onExited?.Invoke(context);
    public void Link(ActionNode prev) => prev.AddListener(Enter);

    public void AddListener(Action<ActionContext> action) => _onExited += action;
    public void RemoveListener(Action<ActionContext> action) => _onExited -= action;
    public void RemoveAllListener() => _onExited = null;
}

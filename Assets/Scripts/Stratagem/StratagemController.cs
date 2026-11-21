using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class StratagemController : MonoBehaviour
{
    private Dictionary<StratagemDataSO,List<ActionNode>> _action = new();
    private StratagemInputBuffer _buffer;

    private void OnEnable() => _buffer.BindEvents();
    private void OnDisable() => _buffer.UnBindEvents();

    [Inject]
    public void Inject(StratagemSlotSO slotSO, StratagemInputModel stratagemInput, SwitchInputModel switchInput, IStratagemFactory factory)
    {
        _action = factory.Create(slotSO);
        _buffer = new(stratagemInput, switchInput, _action);
    }
}

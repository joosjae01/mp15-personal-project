using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

public class StratagemInputBuffer : IStartable, IDisposable
{
    public List<StratagemDataSO> Stratagems;

    private StratagemInputModel _stratagemInput;
    private SwitchInputModel _switchInput;
    private StratagemCoolDown _coolDown;
    private Dictionary<StratagemDataSO, List<ActionNode>> _actionDict;
    private Dictionary<int, StratagemDataSO> _candidate = new();

    private ActionNode _elected = null;
    private StratagemDataSO _electedSO = null;
    private int _count;

    public event Action<StratagemDataSO, int> OnProgress;
    public event Action<StratagemDataSO> OnDropped;
    public event Action OnReset;

    public StratagemInputBuffer(StratagemInputModel stratagemInput, SwitchInputModel switchInput, StratagemCoolDown coolDown, StratagemSlotSO slotSO, IStratagemFactory factory)
    {
        _stratagemInput = stratagemInput;
        _switchInput = switchInput;
        _coolDown = coolDown;
        _actionDict = factory.Create(slotSO);
        Stratagems = new List<StratagemDataSO>(_actionDict.Keys);
    }

    public void Start() => BindEvents();
    public void Dispose() => UnBindEvents();

    private void Init()
    {
        _candidate = new();
        _elected = null;
        _electedSO = null;

        int index = 0;
        foreach (StratagemDataSO dataSO in _actionDict.Keys)
        {
            if (!_coolDown.IsReady(dataSO)) continue;

            _candidate.Add(index, dataSO);
            index++;
        }
    }

    private void CheckInput(Vector2 input)
    {
        Command result = GetInput(input);
        if (result == Command.INVALID) return;
        List<int> toRemove = new();
        int loop = _candidate.Count;

        foreach (KeyValuePair<int, StratagemDataSO> command in _candidate)
        {
            if (command.Value.Command.Count <= _count || command.Value.Command[_count] != result)
            {
                toRemove.Add(command.Key);
                Debug.Log($"{command.Value.Name} : 후보 탈락");
                OnDropped?.Invoke(command.Value);
                _elected = null;
            }
            else
            {
                OnProgress?.Invoke(command.Value, _count + 1);
                if (command.Value.Command.Count == _count + 1)
                {
                    _elected = _actionDict[command.Value][0];
                    _electedSO = command.Value;
                    Debug.Log($"{command.Value.Name} : 준비");
                }
            }
        }

        foreach (int index in toRemove)
        {
            _candidate.Remove(index);
        }

        _count++;
    }

    private Command GetInput(Vector2 input)
    {
        int x = Mathf.RoundToInt(input.x);
        int y = Mathf.RoundToInt(input.y);
        Command result = (x, y) switch
        {
            (0, 1) => Command.UP,
            (0, -1) => Command.DOWN,
            (1, 0) => Command.RIGHT,
            (-1, 0) => Command.LEFT,
            _ => Command.INVALID
        };

        return result;
    }

    public void PerformAction(bool active)
    {
        if (active) Init();
        if (!active && _elected != null)
        {
            _elected.Perform();
            _coolDown.StartCool(_electedSO);
        }

        OnReset?.Invoke();

        _count = 0;
    }

    private void BindEvents()
    {
        _stratagemInput.CommandInput.AddListener(CheckInput);
        _switchInput.OnCommand.AddListener(PerformAction);
    }

    private void UnBindEvents()
    {
        _stratagemInput.CommandInput.RemoveListener(CheckInput);
        _switchInput.OnCommand.RemoveListener(PerformAction);
    }
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StratagemInputBuffer
{
    private StratagemInputModel _stratagemInput;
    private SwitchInputModel _switchInput;

    private Dictionary<StratagemDataSO, List<ActionNode>> _actionDict;
    private Dictionary<int, StratagemDataSO> _candidate = new();
    
    private ActionNode _elected = null;
    private int _count;

    public StratagemInputBuffer(StratagemInputModel stratagemInput, SwitchInputModel switchInput, Dictionary<StratagemDataSO, List<ActionNode>> actionDict)
    {
        _stratagemInput = stratagemInput;
        _switchInput = switchInput;
        _actionDict = actionDict;
    }

    private void Init()
    {
        _candidate = new();
        _elected = null;

        int index = 0;
        foreach(StratagemDataSO dataSO in _actionDict.Keys)
        {
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

        foreach(KeyValuePair<int, StratagemDataSO> command in _candidate)
        {
            if (command.Value.Command.Count <= _count)
            {
                toRemove.Add(command.Key);
                Debug.Log($"{command.Value.Name} : 후보 탈락");
                _elected = null;
            }
            else if (command.Value.Command[_count] != result)
            {
                toRemove.Add(command.Key);
                Debug.Log($"{ command.Value.Name} : 후보 탈락");
                _elected = null;
            }
            else if(command.Value.Command.Count == _count + 1)
            {
                _elected = _actionDict[command.Value][0];
                Debug.Log($"{command.Value.Name} : 준비");
            }
        }

        foreach(int index in toRemove)
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
            (0,1) => Command.UP,
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
        if (!active) _elected?.Enter(_elected.Context);

        _count = 0;
    }

    public void BindEvents()
    {
        _stratagemInput.CommandInput.AddListener(CheckInput);
        _switchInput.OnCommand.AddListener(PerformAction);
    }

    public void UnBindEvents()
    {
        _stratagemInput.CommandInput.RemoveListener(CheckInput);
        _switchInput.OnCommand.RemoveListener(PerformAction);
    }
}

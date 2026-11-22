using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using VContainer;

public class PodCommandInput
{
    private PodCommandUI _ui;
    private StratagemInputModel _model;
    private InputManager _inputManager;
    private StratagemInputBuffer _buffer;

    private List<Command> _sequence;
    private Action _onSuccess;
    private Action _onCancel;
    private int _index;

    public bool IsRunning { get; private set; }

    [Inject]
    public PodCommandInput(StratagemInputModel model, InputManager inputManager, StratagemInputBuffer buffer)
    {
        _model = model;
        _inputManager = inputManager;
        _buffer = buffer;
    }

    public void SetUI(PodCommandUI ui)
    {
        _ui = ui;
    }

    public void Begin(List<Command> sequence, Action onSuccess, Action onCancel)
    {
        _sequence = sequence;
        _onSuccess = onSuccess;
        _onCancel = onCancel;
        _index = 0;
        IsRunning = true;
        _ui.Show(_sequence);

        _buffer.IsBlocked = true;
        _inputManager.ActiveStratagem(true);
        _model.CommandInput.AddListener(OnInput);
    }

    private void OnInput(Vector2 input)
    {
        Command command = ConvertToCommand(input);
        if (command == Command.INVALID) return;

        if(command != _sequence[_index])
        {
            _index = 0;
            _ui.SetProgress(0);
            End(false);
            return;
        }

        _index++;
        _ui.SetProgress(_index);
        if(_index >= _sequence.Count)
        {
            End(true);
        }
    }

    public void Cancel()
    {
        if (IsRunning)
        {
            End(false);
        }
    }

    private void End(bool success)
    {
        _model.CommandInput.RemoveListener(OnInput);
        _inputManager.ActiveStratagem(false);
        _buffer.IsBlocked = false;
        IsRunning = false;
        _ui.Hide();

        if (success) _onSuccess?.Invoke();
        else _onCancel?.Invoke();
    }

    private Command ConvertToCommand(Vector2 value)
    {
        int x = Mathf.RoundToInt(value.x);
        int y = Mathf.RoundToInt(value.y);
        return (x, y) switch
        {
            (0, 1) => Command.UP,
            (0, -1) => Command.DOWN,
            (1, 0) => Command.RIGHT,
            (-1, 0) => Command.LEFT,
            _ => Command.INVALID
        };
    }
}

using System;
using UnityEngine;
using VContainer.Unity;

public class Escape : ITarget, ITickable
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public ObservableProperty<bool> IsCompleted { get; private set; }
    public ObservableProperty<bool> IsFailed { get; private set; }

    public string Condition => $"제한 시간 : {_timeLimit - _elapsed:F0}s";

    private float _timeLimit;
    private float _elapsed;
    private bool _isRunning;

    public event Action OnProgress;

    public Escape(TargetContext context)
    {
        Name = context.Name;
        Desc = context.Desc;
        _timeLimit = context.IntParameter;
        IsCompleted = new(false);
        IsFailed = new(false);
    }

    public void Start()
    {
        _isRunning = true;
    }

    public void Tick()
    {
        if (!_isRunning) return;

        _elapsed += Time.deltaTime;
        OnProgress?.Invoke();
        if(_elapsed >= _timeLimit) Fail();
    }

    public void Complete()
    {
        _isRunning = false;
        IsCompleted.Value = true;
    }

    public void Fail()
    {
        _isRunning = false;
        IsFailed.Value = true;
    }
}

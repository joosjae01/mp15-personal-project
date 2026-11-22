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

    private PlayerController _player;
    private PlayerDataModel _playerData;
    private float _timeLimit;
    private float _elapsed;
    private bool _isRunning;
    private bool _isZoneReady;
    private Vector3 _zonePos;
    private float _zoneRadius;

    public event Action OnProgress;

    public Escape(TargetContext context)
    {
        Name = context.Name;
        Desc = context.Desc;
        _timeLimit = context.IntParameter;
        IsCompleted = new(false);
        IsFailed = new(false);
        _playerData = context.PlayerData;
        _player = context.PlayerController;
    }

    public void Start()
    {
        _isRunning = true;
        _playerData.IsDead.AddListener(CheckFail);
        _player.SetEscape(this);
    }

    public void OnBeacon(Vector3 pos, float radius)
    {
        _zonePos = pos;
        _zoneRadius = radius;
        _isZoneReady = true;
    }

    public void Tick()
    {
        if (!_isRunning) return;

        _elapsed += Time.deltaTime;
        OnProgress?.Invoke();

        if(_elapsed >= _timeLimit)
        {
            Fail();
            return;
        }

        if (!_isZoneReady) return;

        Vector3 pos = _player.transform.position;
        pos.y = 0.0f;
        if((pos - _zonePos).sqrMagnitude <= _zoneRadius * _zoneRadius)
        {
            Complete();
        }
    }

    private void CheckFail(bool isFailed)
    {
        if (isFailed)
        {
            Fail();
        }
    }

    public void Complete()
    {
        _isRunning = false;
        IsCompleted.Value = true;
        _playerData.IsDead.RemoveListener(CheckFail);
    }

    public void Fail()
    {
        _isRunning = false;
        IsFailed.Value = true;
        _playerData.IsDead.RemoveListener(CheckFail);
    }
}

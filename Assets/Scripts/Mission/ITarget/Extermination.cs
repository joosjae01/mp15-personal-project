using System;
using UnityEngine;

public class Extermination : ITarget
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public ObservableProperty<bool> IsCompleted { get; private set; }
    public ObservableProperty<bool> IsFailed { get; private set; }

    public string Condition => $"처치 수 : {CurrentCount} / {GoalCount}";

    private ISpawner _spawner;
    private PlayerDataModel _playerData;
    public int GoalCount;
    public int CurrentCount;

    public event Action OnProgress;

    public Extermination(TargetContext context)
    {
        Name = context.Name;
        Desc = context.Desc;
        IsCompleted = new(false);
        IsFailed = new(false);

        _spawner = context.Spawner;
        _playerData = context.PlayerData;
        GoalCount = context.IntParameter;
        CurrentCount = 0;
    }

    public void GiveUp()
    {
        Fail();
    }

    public void CheckAlive(bool IsDead)
    {
        if (IsDead) Fail();
    }

    public void IncreaseCount(bool IsKilled)
    {
        if (!IsKilled || IsCompleted.Value) return;
        CurrentCount++;
        OnProgress?.Invoke();

        if (CurrentCount >= GoalCount)
        {
            Complete();
        }
    }

    public void Start()
    {
        _spawner.StartSpawn(IncreaseCount);
        _playerData.IsDead.AddListener(CheckAlive);
    }

    public void Complete()
    {
        if (IsCompleted.Value) return;

        _spawner.StopSpawn();
        IsCompleted.Value = true;
        _playerData.IsDead.RemoveListener(CheckAlive);
    }

    public void Fail()
    {
        IsFailed.Value = true;
        _playerData.IsDead.RemoveListener(CheckAlive);
    }
}

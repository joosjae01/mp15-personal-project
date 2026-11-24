using System;
using UnityEngine;

public class Domination : ITarget
{
    public string Name { get; private set; } = "점령";
    public string Desc { get; private set; } = "적들의 기지를 점령하라";
    public string Condition { get; private set; } = $"점령도 : {1:1F}%";

    public ObservableProperty<bool> IsCompleted  { get; private set; }
    public ObservableProperty<bool> IsFailed { get; private set; }

    public event Action OnProgress;

    public void Start()
    {
        IsCompleted = new(false);
        IsFailed = new(false);
    }

    public void Complete()
    {
        IsCompleted.Value = true;
    }

    public void Fail()
    {
        IsFailed.Value = true;
    }
}

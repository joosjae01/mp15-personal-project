using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "IncreaseTime", story: "Increase [Elapsed]", category: "Action", id: "1cf2824062330a51c5e2f968ccbb916e")]
public partial class IncreaseTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Elapsed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Elapsed.Value += Time.deltaTime;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


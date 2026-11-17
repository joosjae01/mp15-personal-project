using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StopNav", story: "Stop [Agent]", category: "Action", id: "2506f92509a3496c01369a4fbd5871ea")]
public partial class StopNavAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Agent;

    protected override Status OnStart()
    {
        Agent.Value.isStopped = true;
        Agent.Value.velocity = Vector3.zero;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


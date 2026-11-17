using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartNav", story: "Start [Agent]", category: "Action", id: "022e9b1150fe9ac8b37bb26c831e966a")]
public partial class StartNavAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Agent;

    protected override Status OnStart()
    {
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


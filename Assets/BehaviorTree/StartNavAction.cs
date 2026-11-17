using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartNav", story: "Start [Agent] with [MoveSpeed] to [Player]", category: "Action", id: "022e9b1150fe9ac8b37bb26c831e966a")]
public partial class StartNavAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Agent;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    [SerializeReference] public BlackboardVariable<PlayerController> Player;
    protected override Status OnStart()
    {
        if (Agent.Value.isStopped)
        {
            Agent.Value.isStopped = false;
            Agent.Value.speed = MoveSpeed;
            Agent.Value.updateRotation = false;
        }
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


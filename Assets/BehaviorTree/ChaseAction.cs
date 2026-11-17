using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Self] chase [Target] with [Agent]", category: "Action", id: "eff7a16f4e6619d8c20e00a19a9c5d59")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Self;
    [SerializeReference] public BlackboardVariable<PlayerController> Target;
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


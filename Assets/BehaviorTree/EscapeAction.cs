using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Escape", story: "[Enemy] run away from [Player] with [Agent]", category: "Action", id: "5fd07399879672f3b7a0e44a8348e774")]
public partial class EscapeAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Enemy;
    [SerializeReference] public BlackboardVariable<PlayerController> Player;
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


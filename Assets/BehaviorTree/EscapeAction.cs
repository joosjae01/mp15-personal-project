using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Escape", story: "[Enemy] run away [Distance] from [Player] with [Agent] and [MoveSpeed]", category: "Action", id: "5fd07399879672f3b7a0e44a8348e774")]
public partial class EscapeAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<PlayerController> Player;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> Agent;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;
    protected override Status OnStart()
    {
        Vector3 runPoint = (Enemy.Value.transform.position - Player.Value.transform.position) * Distance;
        Enemy.Value.transform.LookAt(runPoint);
        Agent.Value.speed = MoveSpeed * 5.0f;
        Agent.Value.isStopped = false;
        Agent.Value.SetDestination(runPoint);
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


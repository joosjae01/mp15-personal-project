using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateNavMesh", story: "Update [NavMeshAgent] to [Player]", category: "Action", id: "862b13bf73fbc17ca895c54d2130471e")]
public partial class UpdateNavMeshAction : Action
{
    [SerializeReference] public BlackboardVariable<NavMeshAgent> NavMeshAgent;
    [SerializeReference] public BlackboardVariable<PlayerController> Player;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        NavMeshAgent.Value.SetDestination(Player.Value.transform.position);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


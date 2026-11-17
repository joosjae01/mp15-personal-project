using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Enemy] Attack [Player] with [AttackBase]", category: "Action", id: "1e2ac153218e1f3c7b11144e1f0781a5")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Enemy;
    [SerializeReference] public BlackboardVariable<PlayerController> Player;
    [SerializeReference] public BlackboardVariable<EnemyAttackBase> AttackBase;
    protected override Status OnStart()
    {
        AttackBase.Value.Attack(Player, Enemy);
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


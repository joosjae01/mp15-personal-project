using System.Collections;
using UnityEngine;

public sealed class MockAttack : EnemyAttackBase
{
    private WaitForSeconds _waitForAttack = new(2.0f);
    public override void Attack(PlayerController player, EnemyController enemy)
    {
        //Debug.Log($"{name} : 공격 발생");
    }
}

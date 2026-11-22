using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "SO/Stratagem/StratagemAction/Slow")]
public class AApplySlow : StratagemActionSO
{
    [SerializeField] private float _slowRate;
    [SerializeField] private float _duration;

    public override void Enter(ActionContext context)
    {
        foreach(EnemyController enemy in context.Targets)
        {
            context.Caster.StartCoroutine(ApplySlowRoutine(enemy));
        }
    }

    private IEnumerator ApplySlowRoutine(EnemyController enemy)
    {
        enemy.NavMeshAgent.speed = enemy.DataModel.MoveSpeed * _slowRate;

        float elapsed = 0f;
        while (elapsed < _duration)
        {
            if (enemy == null || enemy.DataModel == null) yield break;

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (enemy != null && enemy.DataModel != null)
        {
            enemy.NavMeshAgent.speed = enemy.DataModel.MoveSpeed;
        }
    }
}

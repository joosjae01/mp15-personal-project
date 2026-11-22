using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

[CreateAssetMenu(fileName = "AWaitTrigger", menuName = "SO/Stratagem/StratagemAction/WaitTrigger")]
public class AWaitTrigger : StratagemActionSO
{
    [SerializeField] private float _activeDelay;
    [SerializeField] protected float _radius;
    [SerializeField] protected float _lifeTime;

    public override void Enter(ActionContext context)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Execute(ActionContext context)
    {
        yield return new WaitForSeconds(_activeDelay);

        float elapsed = 0.0f;
        while (elapsed < _lifeTime)
        {
            foreach(KeyValuePair<GameObject, EnemyController> enemy in context.Spawner.GetEnemies())
            {
                float square = (enemy.Key.transform.position - context.TargetPos).sqrMagnitude;
                if (square <= _radius * _radius) yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

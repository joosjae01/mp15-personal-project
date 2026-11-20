using UnityEngine;

[CreateAssetMenu(fileName = "AScan", menuName = "SO/Stratagem/StratagemAction/AScan")]
public class AScan : StratagemActionSO
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _targetMask;

    public override void Enter(ActionContext context)
    {
        Debug.Log($"{name} : 스캔 액션 확인");
        Collider[] colliders = Physics.OverlapSphere(context.TargetPos, _radius, _targetMask);

        foreach(Collider collider in colliders)
        {
            EnemyController enemy = context.Spawner.GetEnemy(collider.gameObject);
            context.Targets.Add(enemy);
        }
    }
}

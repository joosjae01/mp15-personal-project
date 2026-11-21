using System.Collections;
using UnityEngine;
using VContainer;

[CreateAssetMenu(fileName = "AVFX", menuName = "SO/Stratagem/StratagemAction/VFX")]
public class AVFX : StratagemActionSO
{
    [SerializeField] private GameObject _VFX;
    [SerializeField] private bool _canChase;
    [SerializeField] private bool _toMonster;

    public override void Enter(ActionContext context)
    {
        Debug.Log($"{name} : VFX 액션 확인");

        if (!_canChase) ApplyToPosition(context);
        else if(_canChase)
        {
            if (_toMonster) ApplyToEnemy(context);
            else ApplyToPlayer(context);
        }
    }

    private void ApplyToPosition(ActionContext context)
    {
        IPoolable clone = context.PoolService.Take(_VFX);
        clone.GameObject.transform.position = context.TargetPos;
    }

    private void ApplyToPlayer(ActionContext context)
    {
        IPoolable clone = context.PoolService.Take(_VFX);
        clone.GameObject.GetComponent<VFXController>().SetTarget(context.Caster.transform);
    }

    private void ApplyToEnemy(ActionContext context)
    {
        foreach(EnemyController enemy in context.Targets)
        {
            IPoolable clone = context.PoolService.Take(_VFX);
            clone.GameObject.transform.position = enemy.transform.position;
            clone.GameObject.GetComponent<VFXController>().SetTarget(enemy.transform);
        }
    }
}

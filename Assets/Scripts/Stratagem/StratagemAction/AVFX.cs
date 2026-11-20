using System.Collections;
using UnityEngine;
using VContainer;

[CreateAssetMenu(fileName = "AVFX", menuName = "SO/Stratagem/StratagemAction/VFX")]
public class AVFX : StratagemActionSO
{
    [SerializeField] private GameObject _VFX;

    public override void Enter(ActionContext context)
    {
        Debug.Log($"{name} : VFX 액션 확인");
        IPoolable clone = context.PoolService.Take(_VFX);
        clone.GameObject.transform.position = context.TargetPos;
    }
}

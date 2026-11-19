using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Creation", menuName = "SO/Stratagem/StratagemAction/Creation")]
public sealed class ACreation : StratagemActionSO
{
    public override void Enter(ActionContext context)
    {
        IPoolable creation = context.poolService.Take(context.CreationPrefab);
        creation.GameObject.transform.SetPositionAndRotation(context.Caster.transform.position, context.Caster.transform.rotation);
        creation.GameObject.TryGetComponent<Rigidbody>(out context.Thrown);
        Debug.Log($"{name} : 생성 액션 확인");
    }
}

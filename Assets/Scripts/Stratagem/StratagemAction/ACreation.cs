using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Creation", menuName = "SO/Stratagem/StratagemAction/Creation")]
public sealed class ACreation : StratagemActionSO
{
    public override void Enter(ActionContext context)
    {
        GameObject prefab = Instantiate(context.ThrownPrefab, context.Caster.transform.position, context.Caster.transform.rotation);
        context.Thrown = prefab.GetComponent<Rigidbody>();
        Debug.Log($"{name} : 생성 액션 확인");
    }
}

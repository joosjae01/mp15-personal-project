using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Throw", menuName = "SO/Stratagem/StratagemAction/Throw")]
public class AThrow : StratagemActionSO
{
    [SerializeField] private float _forwardForce;
    [SerializeField] private float _upwardForce;

    public override void Enter(ActionContext context)
    {
        Debug.Log($"{name} : Throw 액션 확인");
        Vector3 direction = context.Caster.transform.forward * _forwardForce + Vector3.up * _upwardForce;
        context.Thrown.AddForce(direction, ForceMode.Impulse);
    }
}

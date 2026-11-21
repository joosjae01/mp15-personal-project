using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Throw", menuName = "SO/Stratagem/StratagemAction/Throw")]
public class AThrow : StratagemActionSO
{
    [SerializeField] private float _forwardForce;
    [SerializeField] private float _upwardForce;
    [SerializeField] private float _lifeTime;

    public override void Enter(ActionContext context)
    {
        Debug.Log($"{name} : Throw 액션 확인");
        Vector3 direction = context.Caster.transform.forward * _forwardForce + Vector3.up * _upwardForce;
        context.Thrown.AddForce(direction, ForceMode.Impulse);
        context.Caster.StartCoroutine(EndRoutine(context));
    }

    private IEnumerator EndRoutine(ActionContext context)
    {
        yield return new WaitForSeconds(_lifeTime);
        
        Vector3 target = context.Thrown.transform.position;
        target.y = 0;

        context.TargetPos = target;
    }
}

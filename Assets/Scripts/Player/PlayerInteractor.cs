using UnityEngine;
using VContainer;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _range = 1.5f;
    [SerializeField] private LayerMask _mask;

    [Inject] private PlayerInputModel _input;
    private IInteractable _target;

    private void Start() => _input.OnInteract.AddListener(OnInteract);
    private void OnDestroy() => _input.OnInteract.RemoveListener(OnInteract);

    private void Update() => FindTarget();

    private void OnInteract(bool pressed)
    {
        if (pressed) _target?.Interact();
    }

    private void FindTarget()
    {
        _target = null;
        float best = float.MaxValue;

        foreach (Collider hit in Physics.OverlapSphere(transform.position, _range, _mask))
        {
            if (!hit.TryGetComponent(out IInteractable interactable)) continue;

            float dist = (hit.transform.position - transform.position).sqrMagnitude;
            if (dist < best)
            {
                best = dist;
                _target = interactable;
            }
        }
    }
}
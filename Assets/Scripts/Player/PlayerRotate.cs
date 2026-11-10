using UnityEngine;
using VContainer;

public class PlayerRotate : MonoBehaviour
{
    private RayCaster _rayCaster;
    private Vector3 _direction;
    private float _angle;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void RotateToMouse(RaycastHit hit)
    {
        _direction = hit.point - transform.position;
        _direction.y = 0;

        _angle = Mathf.Atan2(_direction.normalized.x, _direction.normalized.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, _angle, 0f);
    }

    private void BindEvents() => _rayCaster.AddListener(RotateToMouse);
    private void UnBindEvents() => _rayCaster.RemoveListener(RotateToMouse);

    [Inject]
    public void Inject(RayCaster rayCaster) => _rayCaster = rayCaster;
}

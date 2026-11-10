using UnityEngine;
using VContainer;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _limitDistance;
    private RayCaster _rayCaster;
    private Vector3 _targetPos;
    private Vector3 _direction;

    private void OnEnable() => BindEvents();
    private void Update() => MoveCamera();
    private void OnDisable() => UnBindEvents();

    private void TranslateCamera(RaycastHit hit)
    {
        _targetPos = (transform.position + hit.point) * 0.5f + _offset;
        _direction = hit.point - transform.position;
        if (Vector3.Distance(hit.point, transform.position) >= _limitDistance)
        {
            _targetPos = transform.position + _direction.normalized * _limitDistance + _offset;
        }
    }

    private void MoveCamera()
    {
        if (_targetPos == default) return;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _targetPos, _cameraSpeed * Time.deltaTime);
    }

    private void BindEvents() => _rayCaster.AddListener(TranslateCamera);
    private void UnBindEvents() => _rayCaster.RemoveListener(TranslateCamera);

    [Inject]
    public void Inject(RayCaster rayCaster)
    {
        _rayCaster = rayCaster;
    }
}

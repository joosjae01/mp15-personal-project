using UnityEngine;
using VContainer;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _limitDistance;
    [Inject] private PlayerInputModel _inputModel;
    private Vector3 _targetPos;
    private Vector3 _direction;

    private void OnEnable() => BindEvents();
    private void Update() => MoveCamera();
    private void OnDisable() => UnBindEvents();

    private void TranslateCamera(Vector3 look)
    {
        _targetPos = (transform.position + look) * 0.5f + _offset;
        _direction = look - transform.position;
        if (Vector3.Distance(look, transform.position) >= _limitDistance) _targetPos = transform.position + _direction.normalized * _limitDistance + _offset;
    }

    private void MoveCamera()
    {
        if (_targetPos != default) Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _targetPos, _cameraSpeed * Time.deltaTime);
    }

    private void BindEvents() => _inputModel.LookDirection.AddListener(TranslateCamera);
    private void UnBindEvents() => _inputModel.LookDirection.RemoveListener(TranslateCamera);
}

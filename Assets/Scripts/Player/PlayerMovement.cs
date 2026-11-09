using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    private PlayerDataModel _dataModel;
    private Vector2 _direction;

    private void OnEnable() => BindEvent();
    private void Update() => Move();
    private void OnDisable() => UnBindEvent();

    private void StartMove(InputAction.CallbackContext context) => _direction = context.ReadValue<Vector2>().normalized;
    private void StopMove(InputAction.CallbackContext context) => _direction = Vector2.zero;
    private void Move() => transform.Translate(_direction * _dataModel.MoveSpeed * Time.deltaTime);

    [Inject]
    public void Inject(PlayerDataModel dataModel) => _dataModel = dataModel;

    private void BindEvent()
    {
        _moveInput.action.Enable();
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
        Debug.Log($"이동속도 : {_dataModel.MoveSpeed}");
    }

    private void UnBindEvent()
    {
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _moveInput.action.Disable();
    }
}

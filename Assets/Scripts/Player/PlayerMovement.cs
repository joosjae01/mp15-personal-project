using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    private PlayerDataModel _dataModel;
    private Vector3 _direction;

    private void OnEnable() => BindEvent();
    private void Update() => Move();
    private void OnDisable() => UnBindEvent();

    private void StartMove(InputAction.CallbackContext context) => _direction = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    private void StopMove(InputAction.CallbackContext context) => _direction = Vector3.zero;
    private void Move() => transform.Translate(_direction.normalized * _dataModel.MoveSpeed * Time.deltaTime);

    [Inject]
    public void Inject(PlayerDataModel dataModel) => _dataModel = dataModel;

    private void BindEvent()
    {
        _moveInput.action.Enable();
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
    }

    private void UnBindEvent()
    {
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _moveInput.action.Disable();
    }
}

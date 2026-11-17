using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerKBM : IInputHandler
{
    private PlayerInputModel _inputModel;
    private InputActionReference _moveInput;
    private InputActionReference _lookInput;
    private LayerMask _targetLayer;

    public InputType Type => InputType.Combat;

    public PlayerKBM(BasicInputAction dataSO, PlayerInputModel inputModel)
    {
        _inputModel = inputModel;
        _moveInput = dataSO.Move;
        _lookInput = dataSO.Look;
        _targetLayer = LayerMask.GetMask("Ground");
    }

    private void GetMoveInput(InputAction.CallbackContext context) => _inputModel.MoveDirection.Value = context.ReadValue<Vector2>();
    private void GetLookInput(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _targetLayer)) _inputModel.LookDirection.Value = hit.point;
    }

    private void StartMove(InputAction.CallbackContext context) => _inputModel.IsMoving.Value = true;
    private void StopMove(InputAction.CallbackContext context) => _inputModel.IsMoving.Value = false;

    public void Enable()
    {
        _moveInput.action.Enable();
        _lookInput.action.Enable();

        _moveInput.action.performed += GetMoveInput;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
        _lookInput.action.performed += GetLookInput;
    }
    public void Disable()
    {
        _moveInput.action.Disable();
        _lookInput.action.Disable();

        _moveInput.action.performed -= GetMoveInput;
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _lookInput.action.performed -= GetLookInput;
    }
}

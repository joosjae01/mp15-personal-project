using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeyboard : IInputHandler
{
    private PlayerInputModel _inputModel;
    private InputActionReference _moveInput;
    private InputActionReference _lookInput;
    private InputActionReference _commandInput;
    private LayerMask _targetLayer;

    public PlayerKeyboard(BasicInputAction dataSO, PlayerInputModel inputModel, LayerMask targetLayer)
    {
        _inputModel = inputModel;
        _moveInput = dataSO.Move;
        _lookInput = dataSO.Look;
        _commandInput = dataSO.Stratagem;
        _targetLayer = targetLayer;
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
    private void StartCommand(InputAction.CallbackContext context) => _inputModel.IsCommand.Value = false;

    public void Enable()
    {
        _moveInput.action.Enable();
        _lookInput.action.Enable();
        _commandInput.action.Enable();

        _moveInput.action.performed += GetMoveInput;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
        _lookInput.action.performed += GetLookInput;
        _commandInput.action.performed += StartCommand;
    }
    public void Disable()
    {
        _moveInput.action.Disable();
        _lookInput.action.Disable();
        _commandInput.action.Disable();

        _moveInput.action.performed -= GetMoveInput;
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _lookInput.action.performed -= GetLookInput;
        _commandInput.action.performed -= StartCommand;
    }
}

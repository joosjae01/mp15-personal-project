using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerKBM : IInputHandler
{
    private PlayerInputModel _inputModel;
    private InputActionReference _moveInput;
    private InputActionReference _lookInput;
    private InputActionReference _interactInput;
    private InputActionReference _escapeInput;
    private LayerMask _targetLayer;

    public InputType Type => InputType.Combat;

    public PlayerKBM(BasicInputAction dataSO, PlayerInputModel inputModel)
    {
        _inputModel = inputModel;
        _moveInput = dataSO.Move;
        _lookInput = dataSO.Look;
        _interactInput = dataSO.Interact;
        _escapeInput = dataSO.Escape;
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
    private void OnInteract(InputAction.CallbackContext context)
    {
        _inputModel.OnInteract.Value = true;
        _inputModel.OnInteract.Value = false;
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        _inputModel.OnEscape.Value = true;
        _inputModel.OnEscape.Value = false;
    }

    public void Enable()
    {
        _moveInput.action.Enable();
        _lookInput.action.Enable();
        _interactInput.action.Enable();
        _escapeInput.action.Enable();

        _moveInput.action.performed += GetMoveInput;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
        _lookInput.action.performed += GetLookInput;
        _interactInput.action.performed += OnInteract;
        _escapeInput.action.performed += OnEscape;
    }
    public void Disable()
    {
        _moveInput.action.Disable();
        _lookInput.action.Disable();
        _interactInput.action.Disable();
        _escapeInput.action.Disable();

        _moveInput.action.performed -= GetMoveInput;
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _lookInput.action.performed -= GetLookInput;
        _interactInput.action.performed -= OnInteract;
        _escapeInput.action.performed -= OnEscape;
    }
}

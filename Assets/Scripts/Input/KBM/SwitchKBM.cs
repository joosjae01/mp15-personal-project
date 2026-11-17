using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchKBM : IInputHandler
{
    public InputType Type => InputType.Switch;
    private InputActionReference _commandInput;
    private SwitchInputModel _inputModel;

    public SwitchKBM(SwitchInputAction inputAction, SwitchInputModel inputModel)
    {
        _commandInput = inputAction.StratagemTrigger;
        _inputModel = inputModel;
    }

    private void StartStratagem(InputAction.CallbackContext context)
    {
        _inputModel.OnCommand.Value = true;
    }

    private void StopStratagem(InputAction.CallbackContext context)
    {
        _inputModel.OnCommand.Value = false;
    }

    public void Enable()
    {
        _commandInput.action.Enable();
        _commandInput.action.performed += StartStratagem;
        _commandInput.action.canceled += StopStratagem;
    }

    public void Disable()
    {
        _commandInput.action.Disable();
        _commandInput.action.performed -= StartStratagem;
        _commandInput.action.canceled -= StopStratagem;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class StratagemKeyboard : IInputHandler
{
    InputActionReference _commandInput;
    InputActionReference _quitInput;

    private StratagemInputModel _inputModel;

    public StratagemKeyboard(StratagemInputAction dataSO, StratagemInputModel inputModel)
    {
        _commandInput = dataSO.Command;
        _quitInput = dataSO.Quit;
        _inputModel = inputModel;
    }

    private void GetStartagemInput(InputAction.CallbackContext context) => _inputModel.CommandInput.Value = context.ReadValue<Vector2>();
    private void StopCommand(InputAction.CallbackContext context) => _inputModel.IsCommand.Value = false;

    public void Disable()
    {
        _commandInput.action.Disable();
        _quitInput.action.Disable();

        _commandInput.action.performed -= GetStartagemInput;
        _quitInput.action.canceled -= StopCommand;
    }

    public void Enable()
    {
        _commandInput.action.Enable();
        _quitInput.action.Enable();

        _commandInput.action.performed += GetStartagemInput;
        _quitInput.action.canceled += StopCommand;
    }
}

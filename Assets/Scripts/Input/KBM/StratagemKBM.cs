using UnityEngine;
using UnityEngine.InputSystem;

public class StratagemKBM : IInputHandler
{
    InputActionReference _commandInput;

    private StratagemInputModel _inputModel;

    public InputType Type => InputType.Stratagem;

    public StratagemKBM(StratagemInputAction dataSO, StratagemInputModel inputModel)
    {
        _commandInput = dataSO.Command;
        _inputModel = inputModel;
    }

    private void GetStartagemInput(InputAction.CallbackContext context) => _inputModel.CommandInput.Value = context.ReadValue<Vector2>();

    public void Disable()
    {
        _commandInput.action.Disable();
        _commandInput.action.performed -= GetStartagemInput;
    }

    public void Enable()
    {
        _commandInput.action.Enable();
        _commandInput.action.performed += GetStartagemInput;
    }
}

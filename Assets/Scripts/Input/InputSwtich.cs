using UnityEngine;
using VContainer;

public class InputSwitch : MonoBehaviour
{
    [Inject] private InputManager _inputManager;
    [Inject] private SwitchInputModel _inputModel;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void SwitchCommand(bool state)
    {
        _inputManager.ActiveStratagem(state);
    }
    private void BindEvents() => _inputModel.OnCommand.AddListener(SwitchCommand);
    public void UnBindEvents() => _inputModel.OnCommand.RemoveListener(SwitchCommand);
}

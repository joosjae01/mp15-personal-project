using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponKBM : IInputHandler
{
    private WeaponInputModel _inputModel;
    private InputActionReference _fireInput;
    private InputActionReference _swapInput;
    private InputActionReference _reloadInput;

    public InputType Type => InputType.Combat;

    public WeaponKBM(BasicInputAction dataSO, WeaponInputModel inputModel)
    {
        _inputModel = inputModel;
        _fireInput = dataSO.Fire;
        _swapInput = dataSO.Swap;
        _reloadInput = dataSO.Reload;
    }

    private void StartFire(InputAction.CallbackContext context) => _inputModel.IsFired.Value = true;
    private void StopFire(InputAction.CallbackContext context) => _inputModel.IsFired.Value = false;
    private void StartReload(InputAction.CallbackContext context) => _inputModel.IsReload.Value = true;
    private void GetsSwapInput(InputAction.CallbackContext context) => _inputModel.SwapValue.Value = context.ReadValue<float>();

    public void Enable()
    {
        _fireInput.action.Enable();
        _reloadInput.action.Enable();
        _swapInput.action.Enable();
        _fireInput.action.performed += StartFire;
        _fireInput.action.canceled += StopFire;
        _reloadInput.action.performed += StartReload;
        _swapInput.action.performed += GetsSwapInput;
    }

    public void Disable()
    {
        _fireInput.action.Disable();
        _reloadInput.action.Disable();
        _swapInput.action.Disable();
        _fireInput.action.performed -= StartFire;
        _fireInput.action.canceled -= StopFire;
        _reloadInput.action.performed -= StartReload;
        _swapInput.action.performed -= GetsSwapInput;
    }
}

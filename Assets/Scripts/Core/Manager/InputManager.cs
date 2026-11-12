using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InputManager : MonoBehaviour, IInputManager
{
    private IInputFactory _inputFactory;

    private List<IInputHandler> _basicInput = new();
    private List<IInputHandler> _strataInput = new();
    private List<IInputHandler> _kbmInput = new();
    private List<IInputHandler> _gamePadInput = new();

    [Inject]
    public void Inject(IInputFactory factory)
    {
        _inputFactory = factory;

        IInputHandler moveKBM = _inputFactory.CreateKeyboard(InputType.Player);
        IInputHandler weaponKBM = _inputFactory.CreateKeyboard(InputType.Weapon);
        IInputHandler strataKBM = _inputFactory.CreateKeyboard(InputType.Stratagem);

        IInputHandler moveGamePad = _inputFactory.CreateGamePad(InputType.Player);
        IInputHandler weaponGamePad = _inputFactory.CreateGamePad(InputType.Weapon);
        IInputHandler strataGamePad = _inputFactory.CreateGamePad(InputType.Stratagem);

        _basicInput.Add(moveKBM);
        _basicInput.Add(weaponKBM);
        _basicInput.Add(moveGamePad);
        _basicInput.Add(weaponKBM);

        _strataInput.Add(strataKBM);
        _strataInput.Add(strataGamePad);

        _kbmInput.Add(moveKBM);
        _kbmInput.Add(weaponKBM);
        _kbmInput.Add(strataKBM);

        _gamePadInput.Add(moveGamePad);
        _gamePadInput.Add(weaponGamePad);
        _gamePadInput.Add(strataGamePad);

        ConnectDevice();
    }

    public void ActiveStratagem(bool flag)
    {
        foreach(IInputHandler inputHandler in _basicInput)
        {
            if (inputHandler == null) continue;

            if (flag) inputHandler.Disable();
            else inputHandler.Enable();

        }

        foreach (IInputHandler inputHandler in _strataInput)
        {
            if (inputHandler == null) continue;

            if (flag) inputHandler.Enable();
            else inputHandler.Disable();
        }
    }


    public void ConnectDevice()
    {
        foreach(IInputHandler inputHandler in _kbmInput)
        {
            if (inputHandler == null) continue;

            inputHandler.Enable();
        }

        ActiveStratagem(false);
    }
}

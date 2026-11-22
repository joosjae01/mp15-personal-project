using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InputManager : MonoBehaviour, IInputManager
{
    [SerializeField] private List<InputContext> _inputContext;
    private Dictionary<InputDevice, List<IInputHandler>> _inputHandlerDict = new();
    private InputDevice _device;
    private IPauseService _pause;
    
    [Inject]
    public void Inject(IInputFactory factory, IPauseService pause)
    {
        _pause = pause;
        foreach(InputContext context in _inputContext) _inputHandlerDict[context.Device] = factory.Create(context);
        ConnectDevice(InputDevice.KBM);
    }

    public void ActiveStratagem(bool active)
    {
        foreach(IInputHandler handler in _inputHandlerDict[_device])
        {
            if(handler.Type == InputType.Stratagem)
            {
                if (active) handler.Enable();
                else handler.Disable();
            }
            else if(handler.Type == InputType.Switch)
            {

            }
            else
            {
                if (active) handler.Disable();
                else handler.Enable();
            }
        }
    }

    public void ConnectDevice(InputDevice device)
    {
        if (!_inputHandlerDict.ContainsKey(device)) return;
        foreach(IInputHandler handler in _inputHandlerDict[_device]) handler.Disable();
        _device = device;
        ActiveStratagem(false);
        foreach(IInputHandler handler in _inputHandlerDict[_device])
        {
            if (handler.Type == InputType.Switch) handler.Enable();
        }
    }

    public void SetPaused(bool paused)
    {
        if (paused)
        {
            foreach(IInputHandler handler in _inputHandlerDict[_device])
            {
                handler.Disable();
            }
        }
        else
        {
            ConnectDevice(_device);
        }
    }
}

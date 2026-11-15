using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InputManager : MonoBehaviour, IInputManager
{
    [SerializeField] private List<InputContext> _inputContext;
    private IInputFactory _inputFactory;
    private Dictionary<InputType, List<IInputHandler>> _inputHandlerDict = new();
    
    [Inject]
    public void Inject(IInputFactory factory)
    {
        _inputFactory = factory;
        foreach(InputContext context in _inputContext) _inputHandlerDict[context.Type] = _inputFactory.Create(context);
        ConnectDevice(InputType.KBM);
    }

    public void ActiveStratagem(InputType type)
    {

    }

    public void ConnectDevice(InputType type)
    {
        if (!_inputHandlerDict.ContainsKey(type)) return;
        foreach(IInputHandler handler in _inputHandlerDict[type]) handler.Enable();
    }
}

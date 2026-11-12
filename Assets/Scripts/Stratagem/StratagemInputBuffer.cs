using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class StratagemInputBuffer : MonoBehaviour
{
    [SerializeField] private int _bufferSize;
    [Inject] private StratagemInputModel _stratagemInput;
    private Stack<Vector2> _inputBuffer;

    private void Start() => Init();
    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void CloseUI(bool isCommand)
    {
        
    }

    private void GetInput(Vector2 input)
    {
        if (input.x != 0 && input.y != 0) return;
        _inputBuffer.Push(input);
    }

    private void Init()
    {
        if(_inputBuffer == null) _inputBuffer = new(_bufferSize);
        _inputBuffer.Clear();
    }

    private void BindEvents()
    {
        _stratagemInput.CommandInput.AddListener(GetInput);
        _stratagemInput.IsCommand.AddListener(CloseUI);
    }

    private void UnBindEvents()
    {
        _stratagemInput.CommandInput.RemoveListener(GetInput);
        _stratagemInput.IsCommand.RemoveListener(CloseUI);
    }
}

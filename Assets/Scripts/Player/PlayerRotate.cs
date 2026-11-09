using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private InputActionReference _rotateInput;
    private Vector2 _mouseScreenPos;
    private Vector3 _look;

    private void OnEnable() => BindEvent();
    private void OnDisable() => UnBindEvent();
    
    private void RotateToMouse(InputAction.CallbackContext context)
    {
        _mouseScreenPos = context.ReadValue<Vector2>();
        _look = new Vector3(_mouseScreenPos.x - Screen.width / 2.0f, 0f, _mouseScreenPos.y - Screen.height / 2.0f);
        if (_look.sqrMagnitude > 0.1f) transform.rotation = Quaternion.LookRotation(_look);
    }

    private void BindEvent()
    {
        _rotateInput.action.Enable();
        _rotateInput.action.performed += RotateToMouse;
    }

    private void UnBindEvent()
    {
        _rotateInput.action.Disable();
        _rotateInput.action.performed -= RotateToMouse;
    }
}

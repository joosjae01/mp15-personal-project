using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private InputActionReference _aimInput;
    [SerializeField] private LayerMask _targetLayer;
    private event Action<RaycastHit> _onRayhitted;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void ShotRay(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, _targetLayer))
        {
            _onRayhitted?.Invoke(hit);
        }
    }

    public void AddListener(Action<RaycastHit> action) => _onRayhitted += action;
    public void RemoveListener(Action<RaycastHit> action) => _onRayhitted -= action;
    private void RemoveAllListeners() => _onRayhitted = null;

    private void BindEvents()
    {
        _aimInput.action.Enable();
        _aimInput.action.performed += ShotRay;
    }

    private void UnBindEvents()
    {
        _aimInput.action.Disable();
        _aimInput.action.performed -= ShotRay;
        RemoveAllListeners();
    }
}

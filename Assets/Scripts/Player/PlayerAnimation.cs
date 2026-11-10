using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _fireInput;
    [SerializeField] private Animator _animator;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void StartMove(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsMoving", true);
        _animator.SetFloat("MoveX", context.ReadValue<Vector2>().normalized.x);
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsMoving", false);
        _animator.SetFloat("MoveX", 0.0f);
    }

    private void StartFire(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsAttacking", true);
    }

    private void StopFire(InputAction.CallbackContext context)
    {
        _animator.SetBool("IsAttacking", false);
    }

    private void BindEvents()
    {
        _moveInput.action.Enable();
        _fireInput.action.Enable();
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
        _fireInput.action.performed += StartFire;
        _fireInput.action.canceled += StopFire;
    }

    private void UnBindEvents()
    {
        _moveInput.action.Disable();
        _fireInput.action.Disable();
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _fireInput.action.performed -= StartFire;
        _fireInput.action.canceled -= StopFire;
    }
}

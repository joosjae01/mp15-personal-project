using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Inject] private PlayerInputModel _playerInput;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void Move(bool isMoving)
    {
        _animator.SetBool("IsMoving", isMoving);
        if (isMoving) _animator.SetFloat("MoveX", 1.0f);
    }

    private void Fire(bool onFire) => _animator.SetBool("IsAttacking", onFire);

    private void BindEvents()
    {
        _playerInput.IsMoving.AddListener(Move);
        //_inputModel.IsFire.AddListener(Fire);
    }

    private void UnBindEvents()
    {
        _playerInput.IsMoving.RemoveListener(Move);
        //_inputModel.IsFire.AddListener(Fire);
    }
}

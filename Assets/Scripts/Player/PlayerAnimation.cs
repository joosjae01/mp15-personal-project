using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Inject] private PlayerInputModel _playerInput;
    [Inject] private WeaponInputModel _weaponInput;

    private void OnEnable() => BindEvents();
    private void OnDisable() => UnBindEvents();

    private void Move(bool isMoving)
    {
        _animator.SetBool("IsMoving", isMoving);
        if (isMoving) _animator.SetFloat("MoveX", 1.0f);
    }

    private void Fire(bool onFire) => _animator.SetBool("IsAttacking", onFire);
    private void Reload(bool onReload) => _animator.SetBool("IsReloading", onReload);

    private void BindEvents()
    {
        _playerInput.IsMoving.AddListener(Move);
        _weaponInput.IsFired.AddListener(Fire);
        _weaponInput.IsReload.AddListener(Reload);
    }

    private void UnBindEvents()
    {
        _playerInput.IsMoving.RemoveListener(Move);
        _weaponInput.IsFired.AddListener(Fire);
        _weaponInput.IsReload.AddListener(Reload);
    }
}

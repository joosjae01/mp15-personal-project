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
    }

    private void Fire(bool onFire) => _animator.SetBool("IsAttacking", onFire);
    private void Reload(bool onReload) => _animator.SetBool("IsReloading", onReload);
    public void Swap(bool onSwap) => _animator.SetBool("OnSwap", onSwap);
    public void ChangeAnimation(int weaponId) => _animator.SetInteger("WeaponId", weaponId);

    private void BindEvents()
    {
        _playerInput.IsMoving.AddListener(Move);
        _weaponInput.IsFired.AddListener(Fire);
        _weaponInput.IsReload.AddListener(Reload);
        _weaponInput.OnSwap.AddListener(Swap);
        _weaponInput.CurrentWeapon.AddListener(ChangeAnimation);
    }

    private void UnBindEvents()
    {
        _playerInput.IsMoving.RemoveListener(Move);
        _weaponInput.IsFired.RemoveListener(Fire);
        _weaponInput.IsReload.RemoveListener(Reload);
        _weaponInput.OnSwap.RemoveListener(Swap);
        _weaponInput.CurrentWeapon.RemoveListener(ChangeAnimation);
    }
}

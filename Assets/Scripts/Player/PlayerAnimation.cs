using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Move(bool isMoving) => _animator.SetBool("IsMoving", isMoving);
    public void Fire(bool onFire) => _animator.SetBool("IsAttacking", onFire);
    public void Swap(bool onSwap) => _animator.SetBool("OnSwap", onSwap);
    public void Reload(bool onReload) => _animator.SetBool("IsReloading", onReload);
    public void ChangeAnimation(int weaponId) => _animator.SetInteger("WeaponId", weaponId);
}

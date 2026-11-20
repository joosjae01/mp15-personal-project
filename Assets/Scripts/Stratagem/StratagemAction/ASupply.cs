using UnityEngine;

[CreateAssetMenu(fileName = "Supply", menuName = "SO/Stratagem/StratagemAction/Supply")]
public class ASupply : StratagemActionSO
{
    [SerializeField] private int _amount;
    public override void Enter(ActionContext context)
    {
        context.WeaponSlot.CurrentWeapon.Model.AmmoClip += _amount;
    }
}

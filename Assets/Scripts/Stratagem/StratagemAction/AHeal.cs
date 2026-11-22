using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "SO/Stratagem/StratagemAction/Heal")]
public class AHeal : StratagemActionSO
{
    [SerializeField] private int _amount;
    public override void Enter(ActionContext context)
    {
        if(context.PlayerData.CurrentHealth.Value + _amount > context.PlayerData.MaxHealth)
        {
            context.PlayerData.CurrentHealth.Value = context.PlayerData.MaxHealth;
        }
        else
        {
            context.PlayerData.CurrentHealth.Value += _amount;
        }
    }
}

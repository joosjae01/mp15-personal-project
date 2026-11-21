using UnityEngine;
using VContainer;

public class PlayerHealth : IDamageable
{
    private PlayerDataModel _dataModel;

    [Inject]
    public PlayerHealth(PlayerDataModel dataModel)
    {
        _dataModel = dataModel;
    }
    
    public void TakeDamage(float damage)
    {
        if (_dataModel.IsDead.Value) return;

        if(_dataModel.CurrentHealth.Value > damage)
        {
            _dataModel.CurrentHealth.Value -= (int)damage;
        }
        else
        {
            _dataModel.CurrentHealth.Value = 0;
            _dataModel.IsDead.Value = true;
        }
    }
}

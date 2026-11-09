using UnityEngine;
using VContainer;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerDataModel _dataModel;

    public void TakeDamage(float damage)
    {
        if (_dataModel.IsDead.Value) return;

        if(_dataModel.CurrentHealth.Value > damage)
        {
            _dataModel.CurrentHealth.Value -= (int)damage;
        }
        else
        {
            _dataModel.IsDead.Value = true;
        }
    }

    [Inject]
    public void Inject(PlayerDataModel dataModel)
    {
        _dataModel = dataModel;
    }
}

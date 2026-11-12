using UnityEngine;
using VContainer;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Inject] private PlayerDataModel _dataModel;

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
}

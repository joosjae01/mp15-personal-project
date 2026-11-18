public class EnemyHealth : IDamageable
{
    private EnemyController _controller;
    private EnemyDataModel _dataModel;

    public EnemyHealth(EnemyController controller, EnemyDataModel dataModel)
    {
        _controller = controller;
        _dataModel = dataModel;
    }

    public void TakeDamage(float damage)
    {
        if(_dataModel.Shield.Value > 0)
        {
            if (_dataModel.Shield.Value >= damage) _dataModel.Shield.Value -= (int)damage;
            else
            {
                _dataModel.Shield.Value = 0;
                _dataModel.CurrentHealth.Value -= (int)damage - _dataModel.Shield.Value;
                if(_dataModel.CurrentHealth.Value <= 0)
                {
                    _dataModel.IsKilled.Value = true;
                    _dataModel.IsKilled.RemoveAllListener();
                    _controller.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if(_dataModel.CurrentHealth.Value > damage)
            {
                _dataModel.CurrentHealth.Value -= (int)damage;
            }
            else
            {
                _dataModel.IsKilled.Value = true;
                _dataModel.IsKilled.RemoveAllListener();
                _controller.gameObject.SetActive(false);
            }
        }
        
    }
}

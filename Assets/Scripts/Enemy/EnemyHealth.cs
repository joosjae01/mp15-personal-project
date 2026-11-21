using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using VContainer;

public class EnemyHealth : IDamageable
{
    private EnemyController _controller;
    private EnemyDataModel _dataModel;
    private WaitForSeconds _waitForDelay;

    public EnemyHealth(EnemyController controller, EnemyDataModel dataModel)
    {
        _controller = controller;
        _dataModel = dataModel;
        _dataModel.CurrentHealth.AddListener(Die);
    }

    private void Die(int health)
    {
        if (health <= 0 && !_dataModel.IsKilled.Value)
        {
            _dataModel.IsKilled.Value = true;
            _controller.BehaviorAgent.SetVariableValue<bool>("IsDead", true);
            AnimatorStateInfo state = _controller.Animator.GetCurrentAnimatorStateInfo(0);
            float duration = state.length;
            _waitForDelay = new WaitForSeconds(duration);
            _controller.StartCoroutine(DieRoutine());
        }
    }

    public void TakeDamage(float damage)
    {
        if (_dataModel.Shield.Value > 0)
        {
            if (_dataModel.Shield.Value >= damage) _dataModel.Shield.Value -= (int)damage;
            else
            {
                _dataModel.Shield.Value = 0;
                _dataModel.CurrentHealth.Value -= (int)damage - _dataModel.Shield.Value;
            }
        }
        else
        {
            _dataModel.CurrentHealth.Value -= (int)damage;
        }
    }

    private IEnumerator DieRoutine()
    {
        yield return _waitForDelay;
        _controller.Source.Release(_controller);
    }
}

using System;
using System.Collections;
using UnityEngine;
using VContainer;

public class EnemyHitEffect : MonoBehaviour, IDisposable
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _effectPoint;
    private WaitForSeconds _waitForDelay;
    private IPoolService _poolService;
    private IPoolable _effect;
    private EnemyDataModel _dataModel;
    private Coroutine _effectRoutine;

    public void Inject(IPoolService poolService, EnemyDataModel dataModel)
    {
        _poolService = poolService;
        _dataModel = dataModel;
        _waitForDelay = new WaitForSeconds(_delay);
        BindEvents();
    }

    private void StartEffect(int health)
    {
        if (_effectRoutine != null) return;
        if (health == _dataModel.MaxHealth) return;

        _effectRoutine = StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        _effect = _poolService.Take(_prefab);
        (_effect as VFXController).SetTarget(_effectPoint);

        yield return _waitForDelay;
        
        _effect.Source.Release(_effect);
        _effect = null;
        _effectRoutine = null;
    }

    private void BindEvents()
    {
        _dataModel.CurrentHealth.AddListener(StartEffect);
    }

    private void UnBindEvents()
    {
        _dataModel.CurrentHealth.RemoveListener(StartEffect);
    }

    public void Dispose()
    {
        UnBindEvents();
    }
}

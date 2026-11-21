using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MissionController : MonoBehaviour
{
    [SerializeField] private SoundDataSO _bgm;
    public MissionDataModel DataModel { get; private set; }
    private PlayerDataModel _playerData;
    private Queue<ITarget> _targetQueue = new();
    private ITarget _currentTarget;
    public ITarget CurrentTarget { get => _currentTarget; }
    private ISoundService _soundService;

    public event Action<ITarget> OnTargetStarted;
    
    [Inject]
    public void Inject(MissionBoxSO boxSO, ITargetFactory factory, ISoundService soundService, PlayerDataModel playerData)
    {
        _playerData = playerData;
        _soundService = soundService;
        _targetQueue = factory.Create(boxSO.TargetList);
        DataModel = new MissionDataModel(boxSO.Data);
    }

    private void Start()
    {
        InitTarget();
    }

    private void Update()
    {
        if(_currentTarget is ITickable)
        {
            (_currentTarget as ITickable).Tick();
        }
    }

    private void InitTarget()
    {
        _soundService.PlayBGM(_bgm);
        _currentTarget = _targetQueue.Dequeue();
        _currentTarget.IsCompleted.AddListener(StartNextTarget);
        _currentTarget.Start();
        OnTargetStarted?.Invoke(_currentTarget);
        Debug.Log($"{name} : [{_currentTarget.Name}] 임무 시작");
    }

    private void StartNextTarget(bool isCompleted)
    {
        if (isCompleted)
        {
            Debug.Log($"{name} : [{_currentTarget.Name}] 임무 완수");
            _currentTarget.IsCompleted.RemoveListener(StartNextTarget);
            if (_targetQueue.Count == 0)
            {
                DataModel.IsCompleted.Value = true;
                Debug.Log("모든 임무 완수");
            }
            else
            {
                _currentTarget = _targetQueue.Dequeue();
                _currentTarget.IsCompleted.AddListener(StartNextTarget);
                _currentTarget.Start();
                OnTargetStarted?.Invoke(_currentTarget);
                Debug.Log($"{name} : [{_currentTarget.Name}] 임무 시작");
            }
        }
    }
}

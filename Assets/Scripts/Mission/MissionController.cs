using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MissionController : MonoBehaviour
{
    [SerializeField] private SoundDataSO _bgm;
    public MissionDataModel DataModel { get; private set; }
    private Queue<ITarget> _targetQueue = new();
    private ITarget _currentTarget;
    private ISoundService _soundService;
    
    [Inject]
    public void Inject(MissionDataSO dataSO, ITargetFactory factory, ISoundService soundService)
    {
        _soundService = soundService;
        _targetQueue = factory.Create(dataSO.TargetContextList);
        DataModel = new MissionDataModel(dataSO);

        InitTarget();
    }

    private void InitTarget()
    {
        _soundService.PlayBGM(_bgm);
        _currentTarget = _targetQueue.Dequeue();
        _currentTarget.IsCompleted.AddListener(StartNextTarget);
        _currentTarget.Start();
        Debug.Log($"{name} : [{_currentTarget.Name}] 임무 시작");
    }

    private void StartNextTarget(bool isCompleted)
    {
        if (isCompleted)
        {
            _currentTarget.IsCompleted.RemoveListener(StartNextTarget);
            if (_targetQueue.Count == 0) DataModel.IsCompleted.Value = true;
            else
            {
                _currentTarget = _targetQueue.Dequeue();
                _currentTarget.IsCompleted.AddListener(StartNextTarget);
                _currentTarget.Start();
                Debug.Log($"{name} : [{_currentTarget.Name}] 임무 시작");
            }
        }
    }
}

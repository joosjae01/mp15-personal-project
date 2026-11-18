using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MissionController : MonoBehaviour
{
    public MissionDataModel DataModel { get; private set; }
    private Queue<ITarget> _targetQueue = new();
    private ITarget _currentTarget;
    
    [Inject]
    public void Inject(MissionDataSO dataSO, ITargetFactory factory)
    {
        _targetQueue = factory.Create(dataSO.TargetContextList);
        DataModel = new MissionDataModel(dataSO);

        InitTarget();
    }

    private void InitTarget()
    {
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

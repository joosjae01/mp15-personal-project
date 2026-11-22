using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MissionController : MonoBehaviour
{
    [SerializeField] private SoundDataSO _bgm;
    public MissionDataModel DataModel { get; private set; }
    private PlayerDataModel _playerData;
    private GameResultPanel _panelUI;
    private Queue<ITarget> _targetQueue = new();
    private ITarget _currentTarget;
    public ITarget CurrentTarget { get => _currentTarget; }
    private ISoundService _soundService;
    private IPauseService _pauseService;
    private InputManager _input;
    private int Cleared;

    public event Action<ITarget> OnTargetStarted;
    public float Elapsed { get; private set;  }

    [Inject]
    public void Inject(MissionBoxSO boxSO, ITargetFactory factory, ISoundService soundService, IPauseService pauseService, PlayerDataModel playerData, InputManager input)
    {
        _input = input;
        _pauseService = pauseService;
        _playerData = playerData;
        _soundService = soundService;
        _targetQueue = factory.Create(boxSO.TargetList);
        DataModel = new MissionDataModel(boxSO.Data);
    }

    public int GetMissionCount()
    {
        return Cleared;
    }

    private void Start()
    {
        InitTarget();
        Elapsed = 0;
    }

    private void Update()
    {
        if(_currentTarget is ITickable)
        {
            (_currentTarget as ITickable).Tick();
        }

        if (!DataModel.IsCompleted.Value)
        {
            Elapsed += Time.deltaTime;
        }
    }

    private void InitTarget()
    {
        _soundService.PlayBGM(_bgm);
        _currentTarget = _targetQueue.Dequeue();
        _currentTarget.IsCompleted.AddListener(StartNextTarget);
        _currentTarget.IsFailed.AddListener(FinishSession);
        _currentTarget.Start();
        OnTargetStarted?.Invoke(_currentTarget);
        Debug.Log($"{name} : [{_currentTarget.Name}] 임무 시작");
    }

    private void FinishSession(bool isFailed)
    {
        _pauseService.PauseGame();
        _input.SetPaused(true);
        DataModel.IsFailed.Value = true;
        Debug.Log("임무 실패");
    }

    private void StartNextTarget(bool isCompleted)
    {
        if (isCompleted)
        {
            Debug.Log($"{name} : [{_currentTarget.Name}] 임무 완수");
            _currentTarget.IsCompleted.RemoveListener(StartNextTarget);
            _currentTarget.IsFailed.RemoveListener(FinishSession);
            Cleared++;

            if (_targetQueue.Count == 0)
            {
                _pauseService.PauseGame();
                _input.SetPaused(true);
                DataModel.IsCompleted.Value = true;
                Debug.Log("모든 임무 완수");
            }
            else
            {
                _currentTarget = _targetQueue.Dequeue();
                _currentTarget.IsCompleted.AddListener(StartNextTarget);
                _currentTarget.IsFailed.AddListener(FinishSession);
                _currentTarget.Start();
                OnTargetStarted?.Invoke(_currentTarget);
                Debug.Log($"{name} : [{_currentTarget.Name}] 임무 시작");
            }
        }
    }
}

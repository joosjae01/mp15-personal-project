using UnityEngine;
using VContainer;

public class MissionTrackerPanel : MonoBehaviour
{
    [SerializeField] private MissionTrackerEntryUI _prefab;
    [SerializeField] private Transform _root;

    private ITarget _target;
    private MissionController _mission;
    private MissionTrackerEntryUI _currentMission;

    [Inject]
    public void Inject(MissionController mission)
    {
        _mission = mission;
    }

    private void Start()
    {
        _mission.OnTargetStarted += AddTarget;
        if(_mission.CurrentTarget != null)
        {
            AddTarget(_mission.CurrentTarget);
        }
    }

    private void OnDestroy()
    {
        if(_mission != null)
        {
            _mission.OnTargetStarted -= AddTarget;
        }

        if(_target != null)
        {
            _target.OnProgress -= Refresh;
        }
    }

    private void Refresh()
    {
        _currentMission.SetCondition(_target.Condition);
    }

    private void AddTarget(ITarget target)
    {
        if(_currentMission != null) _currentMission.SetDone(true);
        if (_target != null) _target.OnProgress -= Refresh;

        _target = target;
        _currentMission = Instantiate(_prefab, _root);
        _currentMission.Init(target.Name);
        _currentMission.SetCondition(target.Condition);
        _target.OnProgress += Refresh;
    }
}

using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EscapePodActivator : MonoBehaviour
{
    [SerializeField] private EscapePod _pod;
    [SerializeField] private PodCommandUI _ui;

    private MissionController _mission;
    private PodCommandInput _input;
    private PlayerController _player;

    [Inject]
    public void Inject(MissionController mission, PodCommandInput input, PlayerController player)
    {
        _mission = mission;
        _input = input; 
        _player = player;
    }

    private void Start() => _mission.OnTargetStarted += Check;
    private void OnDestroy() => _mission.OnTargetStarted -= Check;

    private void Check(ITarget target)
    {
        if (target is not Escape) return;

        _input.SetUI(_ui);
        _pod.Init(_input, _player);
        _pod.gameObject.SetActive(true);
        _pod.Generate();
    }
}
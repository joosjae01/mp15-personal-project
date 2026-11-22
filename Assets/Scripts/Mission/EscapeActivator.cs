using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class EscapePodActivator : MonoBehaviour
{
    [SerializeField] private EscapePod _pod;
    [SerializeField] private PodCommandUI _ui;

    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _clearRadius;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private int _tryCount;

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

    private bool TryGetPodPoint(out Vector3 point)
    {
        Vector3 center = _player.transform.position;

        for(int i = 0; i < _tryCount; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            float distance = Random.Range(_minDistance, _maxDistance);
            Vector3 candidate = center + new Vector3(direction.x, 0.0f, direction.y) * distance;

            if (!NavMesh.SamplePosition(candidate, out NavMeshHit hit, 3f, NavMesh.AllAreas)) continue;
            if (Physics.CheckSphere(hit.position, _clearRadius, _obstacleLayer)) continue;

            point = hit.position;
            return true;
        }

        point = _pod.transform.position;
        return false;
    }

    private void Check(ITarget target)
    {
        if (target is not Escape) return;

        TryGetPodPoint(out Vector3 position);
        _pod.transform.position = position;

        _input.SetUI(_ui);
        _pod.Init(_input, _player);
        _pod.gameObject.SetActive(true);
        _pod.Generate();
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    [Inject] private IPoolService _poolService;
    [Inject] private PlayerController _playerController;
    [SerializeField] private List<SpawnDataSO> _spawnDataSO;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private int _maxAmount;

    [SerializeField] private float _minRadius = 10f;
    [SerializeField] private float _maxRadius = 20f;
    [SerializeField] private int _tryCount = 10;

    private float _elapsed;
    private bool _isActivated;
    private bool _isCoolOn => _elapsed >= _spawnPeriod;
    private bool _canSpawn => _isActivated && _isCoolOn;

    private Dictionary<GameObject, EnemyController> _currentEnemies = new();
    private Dictionary<int, List<SpawnDataSO>> _spawnTable = new();
    private event Action<bool> _OnKilled;

    private void Update()
    {
        if (_isActivated) _elapsed += Time.deltaTime;
        if (_canSpawn) Spawn();
    }

    public void StartSpawn(Action<bool> action)
    {
        SetTable();
        _OnKilled = action;
        _isActivated = true;
        
    }

    public void StopSpawn()
    {
        _OnKilled = null;
        foreach(KeyValuePair<GameObject, EnemyController> enemy in _currentEnemies)
        {
            enemy.Value.DataModel.IsKilled.RemoveAllListener();
        }
        _isActivated = false;
    }

    private void SetTable()
    {
        foreach(SpawnDataSO dataSO in _spawnDataSO)
        {
            if (!_spawnTable.ContainsKey(dataSO.Weight))
            {
                List<SpawnDataSO> tempList = new();
                _spawnTable[dataSO.Weight] = tempList;
            }

            _spawnTable[dataSO.Weight].Add(dataSO);
        }
    }

    private bool TryGetPoint(out Vector3 point)
    {
        for(int i = 0; i < _tryCount; i++)
        {
            Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;
            float dist = UnityEngine.Random.Range(_minRadius, _maxRadius);
            Vector3 candidate = _playerController.transform.position + new Vector3(dir.x, 0f, dir.y) * dist;

            NavMeshHit hit;
            if(NavMesh.SamplePosition(candidate, out hit, 2f, NavMesh.AllAreas))
            {
                point = hit.position;
                return true;
            }
        }

        point = Vector3.zero;
        return false;
    }

    private void Spawn()
    {
        _elapsed = 0;
        int first = GetIndex();
        if (first == -1) return;

        int second = UnityEngine.Random.Range(0, _spawnTable[first].Count);

        foreach (EnemySquad squad in _spawnTable[first][second].EnemySquadList)
        {
            if (squad.Count + _currentEnemies.Count > _maxAmount) return;

            for (int i = 0; i < squad.Count; i++)
            {
                if (!TryGetPoint(out Vector3 pos)) continue;

                IPoolable temp = _poolService.Take(squad.EnemyPrefab);
                EnemyController enemy = temp.GameObject.GetComponent<EnemyController>();

                enemy.transform.position = pos;
                enemy.NavMeshAgent.enabled = true;
                enemy.NavMeshAgent.Warp(pos);

                enemy.DataModel.IsKilled.AddListener(_OnKilled);
                
                _currentEnemies[enemy.gameObject] = enemy;
            }
        }
    }

    private int GetIndex()
    {
        int index = -1;
        int rand = UnityEngine.Random.Range(0, 5);
        for (int i = rand; i >= 0; i--)
        {
            if (_spawnTable.ContainsKey(i))
            {
                index = i;
                break;
            }
        }

        if (index == -1) return -1;
        return index;
    }

    public Dictionary<GameObject, EnemyController> GetEnemies() => _currentEnemies;

    public EnemyController GetEnemy(GameObject clone) => _currentEnemies[clone];
}

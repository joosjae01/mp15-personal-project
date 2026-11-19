using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    [Inject] private IPoolService _poolService;
    [SerializeField] private List<SpawnDataSO> _spawnDataSO;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private int _maxAmount;
    private float _elapsed;
    private bool _isActivated;
    private bool _isCoolOn => _elapsed >= _spawnPeriod;
    private bool _canSpawn => _isActivated && _isCoolOn;

    private List<EnemyController> _currentEnemies = new();
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
                IPoolable temp = _poolService.Take(squad.EnemyPrefab);
                EnemyController enemy = temp.GameObject.GetComponent<EnemyController>();
                enemy.DataModel.IsKilled.AddListener(_OnKilled);
                enemy.NavMeshAgent.enabled = true;
                _currentEnemies.Add(enemy);
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

    public List<EnemyController> GetCurrentEnemies()
    {
        return _currentEnemies;
    }
}

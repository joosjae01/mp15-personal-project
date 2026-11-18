using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    [Inject] private IObjectResolver _resolver;
    [SerializeField] private List<SpawnDataSO> _spawnDataSO;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private int _maxAmount;
    private float _elapsed;
    private bool _isActivated;
    private bool _isCoolOn => _elapsed >= _spawnPeriod;
    private bool _canSpawn => _isActivated && _isCoolOn;

    private List<EnemyController> _currentEnemies = new();
    private Dictionary<int, List<SpawnDataSO>> _spawnTable = new();

    private void Update()
    {
        if (_isActivated) _elapsed += Time.deltaTime;
        if (_canSpawn) Spawn();
    }

    public void StartSpawn()
    {
        Init();
        _isActivated = true;
    }
    public void StopSpawn() => _isActivated = false;

    private void Init()
    {
        foreach(SpawnDataSO dataSO in _spawnDataSO)
        {
            if (!_spawnTable.ContainsKey(dataSO.Weight))
            {
                List<SpawnDataSO> tempList = new();
                _spawnTable[dataSO.Weight] = tempList;
                _spawnTable[dataSO.Weight].Add(dataSO);
            }
            else
            {
                _spawnTable[dataSO.Weight].Add(dataSO);
            }
            
        }
    }

    private void Spawn()
    {
        _elapsed = 0;
        int rand = Random.Range(0, 5);
        Debug.Log($"{name} : 랜덤 값 {rand} 획득");
        int index = -1;
        for(int i = rand; i >= 0; i--)
        {
            if (_spawnTable.ContainsKey(i))
            {
                index = i;
                Debug.Log($"{name} : 가장 큰 KEY {i} 탐색 성공");
                break;
            }
        }

        if(index == -1) return;

        rand = Random.Range(0, _spawnTable[index].Count);

        foreach(EnemySquad squad in _spawnTable[index][rand].EnemySquadList)
        {
            if (squad.Count + _currentEnemies.Count > _maxAmount) return;

            for(int i = 0; i < squad.Count; i++)
            {
                GameObject temp = _resolver.Instantiate(squad.EnemyPrefab);
                _currentEnemies.Add(temp.GetComponent<EnemyController>());
            }
        }
    }

    public List<EnemyController> GetCurrentEnemies()
    {
        return _currentEnemies;
    }
}

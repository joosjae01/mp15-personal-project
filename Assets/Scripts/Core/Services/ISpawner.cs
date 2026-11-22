using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public void StartSpawn(Action<bool> action);
    public void StopSpawn();
    public bool TryGetEnemy(GameObject clone, out EnemyController enemy);
    public Dictionary<GameObject, EnemyController> GetEnemies();
    public int KillCount { get; }
}
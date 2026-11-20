using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public void StartSpawn(Action<bool> action);
    public void StopSpawn();
    public EnemyController GetEnemy(GameObject clone);
    public Dictionary<GameObject, EnemyController> GetEnemies();
}
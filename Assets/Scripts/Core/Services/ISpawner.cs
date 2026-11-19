using System;
using System.Collections.Generic;

public interface ISpawner
{
    public void StartSpawn(Action<bool> action);
    public void StopSpawn();
    public List<EnemyController> GetCurrentEnemies();
}
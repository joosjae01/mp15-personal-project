using System.Collections.Generic;

public interface ISpawner
{
    public void StartSpawn();
    public void StopSpawn();
    public List<EnemyController> GetCurrentEnemies();
}
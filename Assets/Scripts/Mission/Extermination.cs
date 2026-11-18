public class Extermination : ITarget
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public ObservableProperty<bool> IsCompleted { get; private set; }
    public ObservableProperty<bool> IsFailed { get; private set; }

    private ISpawner _spawner;
    private PlayerDataModel _playerData; 
    public int GoalCount;
    public int CurrentCount;

    public Extermination(TargetContext context)
    {
        Name = context.Name;
        Desc = context.Desc;
        IsCompleted = new(false);
        IsFailed = new(false);

        _spawner = context.Spawner;
        _playerData = context.PlayerData;
        GoalCount = context.IntParameter;
        CurrentCount = 0;
    }

    public void GiveUp()
    {
        Fail();
    }

    public void CheckAlive(bool IsDead)
    {
        if (IsDead) Fail();
    }

    public void IncreaseCount(bool IsKilled)
    {
        if (IsKilled) CurrentCount++;
        if(CurrentCount == GoalCount)
        {
            Complete();
        }
    }

    private void Init()
    {
        foreach(EnemyController enemy in _spawner.GetCurrentEnemies())
        {
            enemy.DataModel.IsKilled.AddListener(IncreaseCount);
        }

        _spawner.StartSpawn();
        _playerData.IsDead.AddListener(CheckAlive);
    }

    private void Clear()
    {
        foreach(EnemyController enemy in _spawner.GetCurrentEnemies())
        {
            enemy.DataModel.IsKilled.RemoveListener(IncreaseCount);
        }

        _spawner.StopSpawn();
        _playerData.IsDead.RemoveListener(CheckAlive);
    }

    public void Start()
    {
        Init();
        _spawner.StartSpawn();
    }

    public void Complete()
    {
        IsCompleted.Value = true;
        Clear();
    }

    public void Fail()
    {
        IsFailed.Value = true;
        Clear();
    }
}

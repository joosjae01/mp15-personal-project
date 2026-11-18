public class EnemyDataModel
{
    public ObservableProperty<int> Shield;
    public ObservableProperty<int> CurrentHealth;
    public ObservableProperty<bool> IsKilled;

    public int MaxHealth;

    public float AttackValue;
    public float CoolDown;
    public float AttackDelay;

    public float AttackRange;
    public float DetectRange;
    public float MinRange;

    public float MoveSpeed;
    public float RunDistance;
    
    public bool IsDetected;
    

    public EnemyDataModel(EnemyDataSO dataSO)
    {
        Shield = new(dataSO.Shield);
        CurrentHealth = new(dataSO.MaxHealth);
        IsKilled = new(false);
        MaxHealth = dataSO.MaxHealth;
        AttackDelay = dataSO.AttackDelay;
        AttackValue = dataSO.AttackValue;
        AttackRange = dataSO.AttackRange;
        DetectRange = dataSO.DetectRange;
        RunDistance = dataSO.RunDistance;
        MoveSpeed = dataSO.MoveSpeed;
        MinRange = dataSO.MinRange;
        CoolDown = dataSO.CoolDown;
    }

    public void Reset(EnemyDataSO dataSO)
    {
        Shield = new(dataSO.Shield);
        CurrentHealth = new(dataSO.MaxHealth);
        IsKilled = new(false);
        MaxHealth = dataSO.MaxHealth;
        AttackDelay = dataSO.AttackDelay;
        AttackValue = dataSO.AttackValue;
        AttackRange = dataSO.AttackRange;
        DetectRange = dataSO.DetectRange;
        RunDistance = dataSO.RunDistance;
        MoveSpeed = dataSO.MoveSpeed;
        MinRange = dataSO.MinRange;
        CoolDown = dataSO.CoolDown;
    }
}

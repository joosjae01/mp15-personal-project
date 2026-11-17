using UnityEngine;

public class EnemyDataModel
{
    public ObservableProperty<int> Shield;
    public ObservableProperty<int> CurrentHealth;
    public ObservableProperty<bool> IsKilled;

    public int MaxHealth;

    public float AttackValue;
    public float MoveSpeed;

    public EnemyDataModel(EnemyDataSO dataSO)
    {
        MaxHealth = dataSO.MaxHealth;
        Shield = new(dataSO.Shield);
        CurrentHealth = new(MaxHealth);

        AttackValue = dataSO.AttackValue;
        MoveSpeed = dataSO.MoveSpeed;
        IsKilled = new(false);
    }
}

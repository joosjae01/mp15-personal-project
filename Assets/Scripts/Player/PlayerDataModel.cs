public class PlayerDataModel
{
    public int MaxHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public ObservableProperty<int> CurrentHealth = new(0);
    public ObservableProperty<bool> IsDead = new(false);

    public PlayerDataModel(PlayerDataSO dataSO)
    {
        MaxHealth = dataSO.MaxHealth;
        CurrentHealth.Value = dataSO.MaxHealth;
        MoveSpeed = dataSO.MoveSpeed;
    }
}

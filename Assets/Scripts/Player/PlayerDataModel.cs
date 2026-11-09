public class PlayerDataModel
{
    public int MaxHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public ObservableProperty<int> CurrentHealth = new(0);
    public ObservableProperty<bool> IsDead = new(false);
    //TODO: 현재 스트라타젬 목록

    public PlayerDataModel(PlayerDataSO dataSO)
    {
        MaxHealth = dataSO.MaxHealth;
        CurrentHealth.Value = dataSO.MaxHealth;
        MoveSpeed = dataSO.MoveSpeed;
    }
}

public class MissionDataModel
{
    public ObservableProperty<bool> IsCompleted;
    public MissionDataModel(MissionDataSO dataSO)
    {
        IsCompleted = new(false);
    }
}

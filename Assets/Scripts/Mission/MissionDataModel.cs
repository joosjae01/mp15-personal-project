public class MissionDataModel
{
    public ObservableProperty<bool> IsCompleted;
    public ObservableProperty<bool> IsFailed;
    public MissionDataModel(MissionDataSO dataSO)
    {
        IsCompleted = new(false);
        IsFailed = new(false);
    }
}

public interface ITarget
{
    public string Name { get; }
    public string Desc { get; }
    public ObservableProperty<bool> IsCompleted { get; }
    public ObservableProperty<bool> IsFailed { get; }
    public void Start();
    public void Complete();
    public void Fail();
}

public class Escape : ITarget
{
    public string Name { get; private set; }
    public string Desc { get; private set; }
    public ObservableProperty<bool> IsCompleted { get; private set; }
    public ObservableProperty<bool> IsFailed { get; private set; }

    private float _elapsed;

    public Escape(TargetContext context)
    {
        Name = context.Name;
        Desc = context.Desc;



        IsCompleted = new(false);
        IsFailed = new(false);
    }

    public void Start()
    {

    }

    public void Complete()
    {
        
    }

    public void Fail()
    {
        
    }
}

using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<SoundService>().As<ISoundService>();
        builder.Register<PauseService>(Lifetime.Singleton).As<IPauseService>();
    }
}

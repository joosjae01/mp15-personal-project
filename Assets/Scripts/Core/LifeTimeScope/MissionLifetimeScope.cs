using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

public class MissionLifetimeScope : LifetimeScope
{
    [SerializeField] PlayerDataSO _playerDataSO;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_playerDataSO);
        builder.Register<PlayerDataModel>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<PlayerHealth>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
    }
}

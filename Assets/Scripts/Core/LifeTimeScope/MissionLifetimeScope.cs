using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MissionLifetimeScope : LifetimeScope
{
    [SerializeField] PlayerDataSO _playerDataSO;
    [SerializeField] BasicInputAction _basicKBM;
    [SerializeField] StratagemInputAction _stratagemKBM;

    protected override void Configure(IContainerBuilder builder)
    {
        //SO 
        builder.RegisterInstance(_playerDataSO);
        builder.RegisterInstance(_basicKBM);
        builder.RegisterInstance(_stratagemKBM);

        //DataModel
        builder.Register<PlayerDataModel>(Lifetime.Scoped);

        //InputModel
        builder.Register<PlayerInputModel>(Lifetime.Scoped);
        builder.Register<WeaponInputModel>(Lifetime.Scoped);
        builder.Register<StratagemInputModel>(Lifetime.Scoped);

        builder.RegisterComponentInHierarchy<InputFactory>().As<IInputFactory>();
        builder.RegisterComponentInHierarchy<InputManager>();

        builder.RegisterComponentInHierarchy<PlayerRotate>();
        builder.RegisterComponentInHierarchy<PlayerCamera>();
        builder.RegisterComponentInHierarchy<PlayerHealth>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<PlayerAnimation>();
    }
}

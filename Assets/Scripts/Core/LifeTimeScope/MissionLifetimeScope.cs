using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MissionLifetimeScope : LifetimeScope
{
    [SerializeField] PlayerDataSO _playerDataSO;
    [SerializeField] WeaponSlotSO _weaponSlotSO;

    protected override void Configure(IContainerBuilder builder)
    {
        //SO 
        builder.RegisterInstance(_playerDataSO);
        builder.RegisterInstance(_weaponSlotSO);

        //DataModel
        builder.Register<PlayerDataModel>(Lifetime.Scoped);

        //Factory
        builder.Register<IInputFactory, InputFactory>(Lifetime.Scoped);
        builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Scoped);

        //InputModel
        builder.Register<PlayerInputModel>(Lifetime.Scoped);
        builder.Register<WeaponInputModel>(Lifetime.Scoped);
        builder.Register<StratagemInputModel>(Lifetime.Scoped);
        
        builder.RegisterComponentInHierarchy<InputManager>();
        builder.RegisterComponentInHierarchy<WeaponController>();
        builder.RegisterComponentInHierarchy<PlayerRotate>();
        builder.RegisterComponentInHierarchy<PlayerCamera>();
        builder.RegisterComponentInHierarchy<PlayerHealth>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<PlayerAnimation>();
    }
}

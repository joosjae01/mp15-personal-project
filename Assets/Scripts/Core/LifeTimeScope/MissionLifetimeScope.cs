using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MissionLifetimeScope : LifetimeScope
{
    [SerializeField] PlayerDataSO _playerDataSO;
    [SerializeField] WeaponSlotSO _weaponSlotSO;
    [SerializeField] StratagemSlotSO _stratagemSlotSO;

    protected override void Configure(IContainerBuilder builder)
    {
        //TEST
        builder.RegisterComponentInHierarchy<EnemyController>();

        //SO 
        builder.RegisterInstance(_playerDataSO);
        builder.RegisterInstance(_weaponSlotSO);
        builder.RegisterInstance(_stratagemSlotSO);

        //DataModel
        builder.Register<PlayerDataModel>(Lifetime.Scoped);

        //Factory
        builder.Register<IInputFactory, InputFactory>(Lifetime.Scoped);
        builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Scoped);
        builder.Register<IStratagemFactory, StratagemFactory>(Lifetime.Scoped);

        //InputModel
        builder.Register<PlayerInputModel>(Lifetime.Scoped);
        builder.Register<WeaponInputModel>(Lifetime.Scoped);
        builder.Register<SwitchInputModel>(Lifetime.Scoped);
        builder.Register<StratagemInputModel>(Lifetime.Scoped);
        
        //Manager
        builder.RegisterComponentInHierarchy<InputManager>();

        //Input
        builder.RegisterComponentInHierarchy<InputSwitch>();

        //Weapon
        builder.RegisterComponentInHierarchy<WeaponController>();
        builder.RegisterComponentInHierarchy<WeaponFire>();

        //Player

        builder.Register<PlayerHealth>(Lifetime.Scoped);

        builder.RegisterComponentInHierarchy<PlayerRotate>();
        builder.RegisterComponentInHierarchy<PlayerCamera>();
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<PlayerAnimation>();
        builder.RegisterComponentInHierarchy<PlayerController>();

        //Stratagem
        builder.RegisterComponentInHierarchy<StratagemController>();
    }
}

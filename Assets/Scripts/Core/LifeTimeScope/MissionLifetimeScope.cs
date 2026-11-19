using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MissionLifetimeScope : LifetimeScope
{
    [SerializeField] PlayerDataSO _playerDataSO;
    [SerializeField] WeaponSlotSO _weaponSlotSO;
    [SerializeField] StratagemSlotSO _stratagemSlotSO;
    [SerializeField] MissionDataSO _missionDataSO;

    [SerializeField] private SoundService _soundService;
    [SerializeField] private PoolService _poolService;

    protected override void Configure(IContainerBuilder builder)
    {
        //GameService
        builder.Register<IPauseService, PauseService>(Lifetime.Singleton);
        builder.RegisterComponent(_soundService).As<ISoundService>();
        builder.RegisterComponent(_poolService).As<IPoolService>();
        
        //SO 
        builder.RegisterInstance(_playerDataSO);
        builder.RegisterInstance(_weaponSlotSO);
        builder.RegisterInstance(_stratagemSlotSO);
        builder.RegisterInstance(_missionDataSO);

        //DataModel
        builder.Register<PlayerDataModel>(Lifetime.Scoped);

        //Factory
        builder.Register<IInputFactory, InputFactory>(Lifetime.Scoped);
        builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Scoped);
        builder.Register<IStratagemFactory, StratagemFactory>(Lifetime.Scoped);
        builder.Register<ITargetFactory, TargetFactory>(Lifetime.Scoped);

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
        builder.RegisterComponentInHierarchy<PlayerWeaponRigger>();

        //Enemy
        builder.RegisterComponentInHierarchy<EnemySpawner>();

        //Stratagem
        builder.RegisterComponentInHierarchy<StratagemController>();

        //Mission
        builder.RegisterComponentInHierarchy<MissionController>();
    }
}

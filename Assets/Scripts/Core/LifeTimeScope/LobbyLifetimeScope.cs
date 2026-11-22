using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerDataSO _playerDataSO;
    [SerializeField] private WeaponSlotSO _weaponSlotSO;
    [SerializeField] private StratagemSlotSO _stratagemSlotSO;
    [SerializeField] private MissionBoxSO _missionBoxSO;
    [SerializeField] private MissionDataSO _missionDataSO;

    protected override void Configure(IContainerBuilder builder)
    {
        // SO
        builder.RegisterInstance(_playerDataSO);
        builder.RegisterInstance(_weaponSlotSO);
        builder.RegisterInstance(_stratagemSlotSO);
        builder.RegisterInstance(_missionBoxSO);
        builder.RegisterInstance(_missionDataSO);

        /// 데이터/입력 모델
        builder.Register<PlayerDataModel>(Lifetime.Scoped);
        builder.Register<PlayerInputModel>(Lifetime.Scoped);
        builder.Register<WeaponInputModel>(Lifetime.Scoped);
        builder.Register<SwitchInputModel>(Lifetime.Scoped);
        builder.Register<StratagemInputModel>(Lifetime.Scoped);

        // 입력
        builder.Register<IInputFactory, InputFactory>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<InputManager>();

        // 플레이어
        builder.RegisterComponentInHierarchy<PlayerMovement>();
        builder.RegisterComponentInHierarchy<PlayerAnimation>();
        builder.RegisterComponentInHierarchy<PlayerInteractor>();
        builder.RegisterComponentInHierarchy<PlayerRotate>();

        // Delivery UI 세팅
        builder.Register<ReadyWeaponVM>(Lifetime.Scoped);
        builder.Register<ReadyMissionVM>(Lifetime.Scoped);
        builder.Register<ReadyStratagemVM>(Lifetime.Scoped);

        // 팩토리 세팅
        builder.Register<IViewFactory, ViewFactory>(Lifetime.Scoped);

        // UI
        builder.RegisterComponentInHierarchy<WeaponSelectionView>();
        builder.RegisterComponentInHierarchy<StratagemSelectionView>();
        builder.RegisterComponentInHierarchy<ReadyToCombatUI>();

        //BGM
        builder.RegisterComponentInHierarchy<ReadyOST>();
    }
}
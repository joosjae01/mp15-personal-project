using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private WeaponSlotSO _weaponSlotSO;
    [SerializeField] private StratagemSlotSO _stratagemSlotSO;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_weaponSlotSO);
        builder.RegisterInstance(_stratagemSlotSO);

        //Delivery UI 세팅
        builder.Register<ReadyWeaponVM>(Lifetime.Scoped);
        builder.Register<ReadyStratagemVM>(Lifetime.Scoped);
    }
}

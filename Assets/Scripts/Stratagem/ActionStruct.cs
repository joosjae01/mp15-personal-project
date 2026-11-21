using System;
using UnityEngine;

[Serializable]
public class ActionStruct
{
    public IPoolService PoolService;
    public ISpawner Spawner;
    public PlayerController Caster;
    public PlayerDataModel PlayerData;
    public WeaponSlot WeaponSlot;
    public GameObject Prefab;
    public float Damage;

    public ActionStruct(IPoolService poolService, ISpawner spawner, PlayerController caster, PlayerDataModel playerData, WeaponSlot weaponSlot, GameObject prefab, float damage)
    {
        PoolService = poolService;
        Spawner = spawner;
        Caster = caster;
        PlayerData = playerData;
        WeaponSlot = weaponSlot;
        Prefab = prefab;
        Damage = damage;
    }
}

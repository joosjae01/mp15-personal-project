using System;
using UnityEngine;

[Serializable]
public class ActionStruct
{
    public IPoolService PoolService;
    public PlayerController Caster;
    public PlayerDataModel PlayerData;
    public WeaponSlot WeaponSlot;
    public GameObject Prefab;
    public float Damage;

    public ActionStruct(IPoolService poolService, PlayerController caster, PlayerDataModel playerData, WeaponSlot weaponSlot, GameObject prefab, float damage)
    {
        PoolService = poolService;
        Caster = caster;
        PlayerData = playerData;
        WeaponSlot = weaponSlot;
        Prefab = prefab;
        Damage = damage;
    }
}

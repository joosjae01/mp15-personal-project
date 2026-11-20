using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionContext
{
    // 정적
    public IPoolService PoolService;
    public PlayerController Caster;
    public PlayerDataModel PlayerData;
    public WeaponSlot WeaponSlot;
    public float Damage;

    // 동적
    public GameObject CreationPrefab;
    public IPoolable Creation;
    public Rigidbody Thrown;

    public List<EnemyController> Targets = new();
    
    public Vector3 TargetPos;

    public ActionContext(ActionStruct actionStruct)
    {
        PoolService = actionStruct.PoolService;
        Caster = actionStruct.Caster;
        PlayerData = actionStruct.PlayerData;
        WeaponSlot = actionStruct.WeaponSlot;
        CreationPrefab = actionStruct.Prefab;
        Damage = actionStruct.Damage;
    }
}

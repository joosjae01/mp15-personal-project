using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionContext
{
    public PlayerController Caster;
    public PlayerDataModel PlayerData;
    public IPoolService poolService;

    public WeaponSlot WeaponSlot;
    public GameObject CreationPrefab;

    public List<EnemyController> Targets = new();
    public Rigidbody Thrown;

    public Vector3 TargetPos;
    public float Damage;
}

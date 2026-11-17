using System;
using UnityEngine;

[Serializable]
public class ActionContext
{
    public PlayerController Caster;
    public PlayerDataModel PlayerData;
    public WeaponSlot WeaponSlot;
    public GameObject Target;
    public GameObject ThrownPrefab;
    public Rigidbody Thrown;
    public PoolableType Type;
    public Vector3 TargetPos;
}

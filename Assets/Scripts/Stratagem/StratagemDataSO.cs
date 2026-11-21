using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StratagemData", menuName = "SO/Stratagem/StratagemData")]
public class StratagemDataSO : ScriptableObject
{
    public string Name;
    public string Desc;
    public float CoolDown;
    public float Damage;
    public Sprite Icon;
    public GameObject Creation;
    public StratagemType Type;
    public List<Command> Command;
    public List<StratagemActionSO> Action;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StratagemData", menuName = "SO/Stratagem/StratagemData")]
public class StratagemDataSO : ScriptableObject
{
    public string Name;
    public string Desc;
    public Sprite Icon;
    public StratagemType Type;
    public List<Command> Command;
    public ActionContext Context;
    public List<StratagemActionSO> Action;
}

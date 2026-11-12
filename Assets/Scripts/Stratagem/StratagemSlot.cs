using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StratagemSlot", menuName = "SO/Delivery/StratagemSlot")]
public class StratagemSlot : ScriptableObject
{
    public List<IStratagem> StratagemList;
}

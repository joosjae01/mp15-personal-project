using System.Collections.Generic;
using UnityEngine;

public interface IStratagemFactory
{
    public Dictionary<StratagemDataSO, List<ActionNode>> Create(StratagemSlotSO slotSO);
}

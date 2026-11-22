using System.Collections.Generic;

public interface IStratagemFactory
{
    public Dictionary<StratagemDataSO, List<ActionNode>> Create(List<StratagemDataSO> slotSO);
}

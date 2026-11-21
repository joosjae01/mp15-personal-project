using System.Collections.Generic;
using VContainer;

public class ReadyMissionVM
{
    private MissionBoxSO _boxSO;
    private MissionDataSO _dataSO;

    [Inject]
    public ReadyMissionVM(MissionBoxSO boxSO, MissionDataSO dataSO)
    {
        _boxSO = boxSO;
        _dataSO = dataSO;
    }

    public List<TargetContext> Roll()
    {
        _boxSO.Data = _dataSO;
        _boxSO.TargetList = _dataSO.GenerateList();
        return _boxSO.TargetList;
    }
}

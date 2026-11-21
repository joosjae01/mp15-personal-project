using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ReadyStratagemVM : IViewModel
{
    private List<StratagemDataSO> _delivery = new();
    private List<StratagemDataSO> _slotSO = new();
    private int _max;

    [Inject]
    public ReadyStratagemVM(StratagemSlotSO slotSO)
    {
        foreach(StratagemDataSO dataSO in slotSO.StratagemData) _slotSO.Add(dataSO);
        _max = 3;
    }

    public List<StratagemDataSO> GetDelivery() => _delivery;
    public bool AddDataSO(int index)
    {
        if (_delivery.Count == _max) return false;
        if (_delivery.Contains(_slotSO[index])) return false;
        _delivery.Add(_slotSO[index]);
        return true;
    }

    public bool RemoveDataSO(int index)
    {
        if (_delivery.Count <= 0) return false;
        _delivery.Remove(_slotSO[index]);
        return true;
    }

    public string GetName(int index) => _slotSO[index].Name;
    public string GetDesc(int index) => _slotSO[index].Desc;
    public Sprite GetImage(int index) => _slotSO[index].Icon;
    public float GetCoolDown(int index) => _slotSO[index].CoolDown;
}

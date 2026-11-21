using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ReadyWeaponVM : IViewModel
{
    private List<WeaponContext> _delivery = new();
    private List<WeaponContext> _dataList = new();
    private int _max;

    [Inject]
    public ReadyWeaponVM(WeaponSlotSO slotSO)
    {
        foreach(WeaponContext context in slotSO.WeaponContext) _dataList.Add(context);
        _max = 3;
    }

    public List<WeaponContext> GetDelivery() => _delivery;
    public void AddContext(int index)
    {
        if (_delivery.Count == _max) return;
        if (_delivery.Contains(_dataList[index])) return;
        _delivery.Add(_dataList[index]);
    }
    public void RemoveContext(int index)
    {
        if (_delivery.Count <= 0) return;
        _delivery.Remove(_dataList[index]);
    }

    public string GetName(int index) => _dataList[index].DataSO.Name;
    public Sprite GetImage(int index) => _dataList[index].DataSO.Icon;
    public int GetPower(int index) => (int)_dataList[index].DataSO.Power;
    public int GetFireRate(int index) => (int) _dataList[index].DataSO.FireRate;
    public int GetStability(int index) => (int)(_dataList[index].DataSO.Stability * 10);
    public int GetMagazine(int index) => _dataList[index].DataSO.MaxMagazine;
    public int GetRange(int index) => (int)_dataList[index].DataSO.Range;
}

using System.Collections.Generic;
using UnityEngine;

public class StratagemCoolDown
{
    private Dictionary<StratagemDataSO, float> _coolDict = new();

    public bool IsReady(StratagemDataSO dataSO)
    {
        if (!_coolDict.TryGetValue(dataSO, out float endTime))
            return true;

        return Time.time >= endTime;
    }

    public float GetRemain(StratagemDataSO dataSO)
    {
        if (!_coolDict.TryGetValue(dataSO, out float endTime))
            return 0f;

        float remaining = endTime - Time.time;

        if (remaining < 0f)
            return 0f;

        return remaining;
    }

    public void StartCool(StratagemDataSO dataSO)
    {
        _coolDict[dataSO] = Time.time + dataSO.CoolDown;
    }
}
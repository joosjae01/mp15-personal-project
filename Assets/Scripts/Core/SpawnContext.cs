using System;

[Serializable]
public struct SpawnContext
{
    public float Period;
    public int SpawnAmount;
    public int MaxAmount;
    public float SpawnAmountRate;
}

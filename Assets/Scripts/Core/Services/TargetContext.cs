using System;

[Serializable]
public struct TargetContext
{
    public string Name;
    public string Desc;
    public TargetType Type;
    public ISpawner Spawner;
    public PlayerDataModel PlayerData;
    public int IntParameter;
}

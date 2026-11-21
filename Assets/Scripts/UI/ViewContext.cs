using System.Collections.Generic;
using UnityEngine;

public struct ViewContext
{
    public GameObject Prefab;
    public Transform Parent;
    public ViewType Type;
    public int Count;

    public ViewContext(GameObject prefab, Transform parent, ViewType type, int count)
    {
        Prefab = prefab;
        Parent = parent;
        Type = type;
        Count = count;
    }
}

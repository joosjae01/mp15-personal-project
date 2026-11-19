using UnityEngine;

public interface IPoolService
{
    public IPoolable Take(GameObject prefab);
    public void Return(IPoolable poolable);
}

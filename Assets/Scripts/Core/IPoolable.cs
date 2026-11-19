using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable
{
    public ObjectPool<IPoolable> Source { get;}
    public GameObject GameObject { get; }
    public void SetSource(ObjectPool<IPoolable> source);
}

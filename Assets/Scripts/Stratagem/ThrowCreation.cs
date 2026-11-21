using UnityEngine;
using UnityEngine.Pool;

public class ThrowCreation : MonoBehaviour, IPoolable
{
    public ObjectPool<IPoolable> Source { get; private set; }

    public GameObject GameObject { get => gameObject; }

    public void SetSource(ObjectPool<IPoolable> source)
    {
        Source = source;
    }
}

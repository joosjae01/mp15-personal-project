using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class PoolService : MonoBehaviour, IPoolService
{
    [SerializeField] private List<PoolContext> _contextList;
    private Dictionary<GameObject, ObjectPool<IPoolable>> _pool = new();
    private IObjectResolver _resolver;

    [Inject]
    public void Inject(IObjectResolver resolver)
    {
        _resolver = resolver;
        Init();
    }

    private void Init()
    {
        foreach(PoolContext context in _contextList)
        {
            GameObject prefab = context.Prefab;
            ObjectPool<IPoolable> tempPool = new(
                () => OnCreate(prefab),
                OnTake,
                OnReturn,
                OnDestroyPoolable,
                false,
                context.Count,
                context.Count
                );

            _pool.Add(context.Prefab, tempPool);

            List<IPoolable> tempList = new();

            for (int i = 0; i < context.Count; i++)
            {
                IPoolable temp = _pool[prefab].Get();
                tempList.Add(temp);
            }

            for(int j = 0; j < context.Count; j++)
            {
                _pool[prefab].Release(tempList[j]);
            }

        }
    }

    private IPoolable OnCreate(GameObject prefab)
    {
        GameObject instance = _resolver.Instantiate(prefab, transform);
        IPoolable poolable = instance.GetComponent<IPoolable>();
        poolable.SetSource(_pool[prefab]);
        return poolable;
    }

    private void OnTake(IPoolable poolable) => poolable.GameObject.SetActive(true);
    private void OnReturn(IPoolable poolable)
    {
        poolable.GameObject.SetActive(false);
    }

    private void OnDestroyPoolable(IPoolable poolable) => Destroy(poolable.GameObject);

    public IPoolable Take(GameObject prefab)
    {
        return _pool[prefab].Get();
    }

    public void Return(IPoolable poolable)
    {
        poolable.Source.Release(poolable);
    }

    
}

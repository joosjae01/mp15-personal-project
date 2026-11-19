using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletTrail : MonoBehaviour, IPoolable, IBullet
{
    [SerializeField] private float _moveSpeed;
    public ObjectPool<IPoolable> Source { get; private set; }
    public GameObject GameObject { get => gameObject; }

    public void SetSource(ObjectPool<IPoolable> source)
    {
        Source = source;
    }

    public void SetTarget(Vector3 target)
    {
        StartCoroutine(MoveToTarget(target));
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, target) <= 0.1)
            {
                Source.Release(this);
            }
            yield return null;
        }
    }
}

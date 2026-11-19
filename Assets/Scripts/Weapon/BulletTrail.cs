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
            if(transform.position == target) Source.Release(this);
            transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

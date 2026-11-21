using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class VFXController : MonoBehaviour, IPoolable
{
    public ObjectPool<IPoolable> Source { get; set; }
    public GameObject GameObject { get => gameObject; }

    private WaitForSeconds _waitForReturn;
    private Coroutine _returnRoutine;
    private Transform _target;

    [SerializeField] private ParticleSystem _particle;

    public void SetSource(ObjectPool<IPoolable> source)
    {
        Source = source;
    }

    private void Awake()
    {
        _waitForReturn = new WaitForSeconds(_particle.main.duration);
    }

    private void OnEnable()
    {
        _target = null;
        _particle.Play();
        StartReturn();
    }

    private void Update()
    {
        if(_target != null)
        {
            transform.position = _target.position;
        }
    }

    private void OnDisable()
    {
        _particle.Stop();
        StopReturn();
    }

    private void StartReturn()
    {
        if (_returnRoutine != null) return;
        _returnRoutine = StartCoroutine(ReturnRoutine());
    }

    private void StopReturn()
    {
        if (_returnRoutine == null) return;
        StopCoroutine(_returnRoutine);
        _returnRoutine = null;
    }

    private IEnumerator ReturnRoutine()
    {
        yield return _waitForReturn;
        Source.Release(this);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}

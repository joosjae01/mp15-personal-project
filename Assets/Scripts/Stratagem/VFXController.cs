using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class VFXController : MonoBehaviour, IPoolable
{
    public ObjectPool<IPoolable> Source { get; set; }
    public GameObject GameObject { get => gameObject; }

    private WaitForSeconds _waitForReturn;
    private Coroutine _returnRoutine;

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
        _particle.Play();
        StartReturn();
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
}

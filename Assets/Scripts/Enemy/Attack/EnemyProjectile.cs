using UnityEngine;
using UnityEngine.Pool;

public class EnemyProjectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _hitRadius;

    public ObjectPool<IPoolable> Source {get; set;}
    public GameObject GameObject => gameObject;

    private PlayerController _player;
    private float _damage;
    private float _elapsed;

    private void Update() => Move();

    public void SetSource(ObjectPool<IPoolable> source)
    {
        Source = source;
    }

    public void Launch(PlayerController player, Vector3 direction, float damage)
    {
        _player = player;
        _damage = damage;
        _elapsed = 0.0f;
        transform.forward = direction;
    }

    public void Move()
    {
        transform.position += transform.forward * (_speed * Time.deltaTime);

        Vector3 target = _player.transform.position + Vector3.up;
        if ((target - transform.position).sqrMagnitude <= _hitRadius)
        {
            _player.Health.TakeDamage(_damage);
            Source.Release(this);
            return;
        }

        _elapsed += Time.deltaTime;
        if( _elapsed > _lifeTime)
        {
            Source.Release(this);
        }
    }
}

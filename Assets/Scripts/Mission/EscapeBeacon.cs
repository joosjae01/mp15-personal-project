using System;
using UnityEngine;

public class EscapeBeacon : MonoBehaviour
{
    [SerializeField] private float _activeDelay;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _laser;
    [SerializeField] private float _radius;
    [field : SerializeField] public Transform RightHand { get; private set; }


    private Rigidbody _rigidBody;
    private Action<Vector3, float> _onCreated;
    private float _started;
    private bool _onCreating;
    private bool _isThrown;
    private bool _isGround;

    private void Awake() => _rigidBody = GetComponent<Rigidbody>();
    private void Update()
    {
        if (!_onCreating || Time.time < _started) return;

        _onCreating = false;
        Create();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isThrown || _isGround) return;
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground")) return;

        _isGround = true;

        _rigidBody.linearVelocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _rigidBody.isKinematic = true;
        transform.rotation = Quaternion.identity;
        _laser.SetActive(true);

        _started = Time.time + _activeDelay;
        _onCreating = true;
    }

    public void Throw(Vector3 force, Action<Vector3, float> onCreated)
    {
        _onCreated = onCreated;
        _isThrown = true;
        _isGround = false;

        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(force, ForceMode.Impulse);
    }

    private void Create()
    {
        Vector3 pos = transform.position;
        pos.y = 0.0f;
        Instantiate(_prefab, pos, Quaternion.identity);
        _onCreated?.Invoke(pos, _radius);
        Destroy(gameObject);
    }
}

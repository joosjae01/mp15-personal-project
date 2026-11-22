using System;
using UnityEngine;

public class EscapeBeacon : MonoBehaviour
{
    [SerializeField] private float _activeDelay;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _radius;
    [field : SerializeField] public Transform RightHand { get; private set; }


    private Rigidbody _rigidBody;
    private Action<Vector3, float> _onCreated;
    private float _started;
    private bool _onCreating;

    private void Awake() => _rigidBody = GetComponent<Rigidbody>();
    private void Update()
    {
        if (!_onCreating || Time.time < _started) return;

        _onCreating = false;
        Create();
    }

    public void Throw(Vector3 force, Action<Vector3, float> onCreated)
    {
        _onCreated = onCreated;
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(force, ForceMode.Impulse);

        _started = Time.time + _activeDelay;
        _onCreating = true;
    }

    private void Create()
    {
        Vector3 pos = transform.position;
        pos.y = 0.0f;
        Instantiate(_prefab, pos, Quaternion.identity);
        _onCreated?.Invoke(pos, _radius);
    }
}

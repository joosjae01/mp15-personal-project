using UnityEngine;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private PlayerDataModel _dataModel;
    [Inject] private PlayerInputModel _inputModel;
    [Inject] private PlayerAnimation _animation;
    private Rigidbody _rigidBody;
    private Vector3 _direction;

    private void Awake() => _rigidBody = GetComponent<Rigidbody>();
    private void OnEnable() => BindEvents();
    private void FixedUpdate()
    {
        if (_inputModel.IsMoving.Value) Move();
        else Stop();
    }
    private void OnDisable() => UnBindEvents();

    private void GetDirection(Vector2 moveInput) => _direction = new(moveInput.x, 0.0f, moveInput.y);
    private void Move()
    {
        Vector3 veloocity = _direction.normalized * _dataModel.MoveSpeed;
        veloocity.y = _rigidBody.linearVelocity.y;
        _rigidBody.linearVelocity = veloocity; 
        _animation.Move(true);
    }

    private void Stop()
    {
        Vector3 velocity = _rigidBody.linearVelocity;
        _rigidBody.linearVelocity = new Vector3(0.0f, velocity.y, 0.0f);
        _animation.Move(false);
    }

    private void BindEvents()
    {
        _inputModel.MoveDirection.AddListener(GetDirection);
    }
    private void UnBindEvents()
    {
        _inputModel.MoveDirection.RemoveListener(GetDirection);
    }
}

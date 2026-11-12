using UnityEngine;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private PlayerDataModel _dataModel;
    [Inject] private PlayerInputModel _inputModel;

    private Vector3 _direction;

    private void OnEnable() => BindEvents();
    private void Update()
    {
        if (_inputModel.IsMoving.Value) Move();
    }
    private void OnDisable() => UnBindEvents();

    private void GetDirection(Vector2 moveInput) => _direction = new(moveInput.x, 0.0f, moveInput.y);
    private void Move() => transform.Translate(_direction.normalized * _dataModel.MoveSpeed * Time.deltaTime, Space.World);

    private void BindEvents()
    {
        _inputModel.MoveDirection.AddListener(GetDirection);
    }
    private void UnBindEvents()
    {
        _inputModel.MoveDirection.RemoveListener(GetDirection);
    }
}

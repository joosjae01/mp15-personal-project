using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    private PlayerDataModel _dataModel;
    private ISoundService _soundManager;
    private Vector3 _direction;

    private void OnEnable() => BindEvent();
    private void Update() => Move();
    private void OnDisable() => UnBindEvent();

    private void StartMove(InputAction.CallbackContext context) => _direction = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    private void StopMove(InputAction.CallbackContext context) => _direction = Vector3.zero;
    private void Move() => transform.Translate(_direction.normalized * _dataModel.MoveSpeed * Time.deltaTime, Space.World);
    private void StartSound(InputAction.CallbackContext context) => _soundManager.PlaySound(SoundEffectType.PlayerWalking);
    private void StopSound(InputAction.CallbackContext context) => _soundManager.PauseSound(SoundEffectType.PlayerWalking);

    [Inject]
    public void Inject(PlayerDataModel dataModel, ISoundService soundManager)
    {
        _dataModel = dataModel;
        _soundManager = soundManager;
    }

    private void BindEvent()
    {
        _moveInput.action.Enable();
        _moveInput.action.started += StartSound;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += StopMove;
        _moveInput.action.canceled += StopSound;
    }

    private void UnBindEvent()
    {
        _moveInput.action.started -= StartSound;
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= StopMove;
        _moveInput.action.canceled -= StopSound;
        _moveInput.action.Disable();
    }
}

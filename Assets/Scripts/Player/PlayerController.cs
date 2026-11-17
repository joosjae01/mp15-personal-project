using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [Inject] private PlayerAnimation _playerAnimation;
    [Inject] private PlayerMovement _playerMovement;
    [Inject] private PlayerCamera _playerCamera;
    [Inject] private PlayerRotate _playerRotate;

    [Inject] private PlayerDataModel _dataModel;
    [Inject] private PlayerHealth _playerHealth;
}

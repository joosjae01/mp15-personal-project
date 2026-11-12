using UnityEngine;
using VContainer;

public class InputFactory : MonoBehaviour, IInputFactory
{
    [Inject] private BasicInputAction _basicKBM;
    [Inject] private StratagemInputAction _strataKBM;
    [Inject] private PlayerInputModel _playerInputModel;
    [Inject] private WeaponInputModel _weaponInputModel;
    [Inject] private StratagemInputModel _strataInputModel;

    [SerializeField] LayerMask _targetLayer;

    public IInputHandler CreateGamePad(InputType type)
    {
        return null;
    }

    public IInputHandler CreateKeyboard(InputType type)
    {
        return type switch
        {
            InputType.Player => new PlayerKeyboard(_basicKBM, _playerInputModel, _targetLayer),
            InputType.Weapon => new WeaponKeyboard(_basicKBM, _weaponInputModel),
            InputType.Stratagem => new StratagemKeyboard(_strataKBM, _strataInputModel),
            _ => null
        };
    }
}

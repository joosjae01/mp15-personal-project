using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InputFactory : IInputFactory
{
    [Inject] private PlayerInputModel _playerInputModel;
    [Inject] private WeaponInputModel _stratagemInputModel;
    [Inject] private StratagemInputModel _strataInputModel;
    [Inject] private SwitchInputModel _switchInputModel;

    public List<IInputHandler> Create(InputContext context)
    {
        List<IInputHandler> temp = new();
        if (context.Device == InputDevice.KBM)
        {
            temp.Add(new PlayerKBM(context.Basic, _playerInputModel));
            temp.Add(new WeaponKBM(context.Basic, _stratagemInputModel));
            temp.Add(new SwitchKBM(context.Switch, _switchInputModel));
            temp.Add(new StratagemKBM(context.Stratagem, _strataInputModel));

            return temp;
        }

        if(context.Device == InputDevice.GamePad)
        {
            //TODO : 게임 패드 로직
            return temp;
        }

        return null;
    }
}

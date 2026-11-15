using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InputFactory : IInputFactory
{
    [Inject] private PlayerInputModel _playerInputModel;
    [Inject] private WeaponInputModel _weaponInputModel;
    [Inject] private StratagemInputModel _strataInputModel;

    public List<IInputHandler> Create(InputContext context)
    {
        List<IInputHandler> temp = new();
        if (context.Type == InputType.KBM)
        {
            temp.Add(new PlayerKeyboard(context.Basic, _playerInputModel));
            temp.Add(new WeaponKeyboard(context.Basic, _weaponInputModel));
            temp.Add(new StratagemKeyboard(context.Stratagem, _strataInputModel));

            return temp;
        }

        if(context.Type == InputType.GamePad)
        {
            //TODO : 게임 패드 로직
            return temp;
        }

        return null;
    }
}

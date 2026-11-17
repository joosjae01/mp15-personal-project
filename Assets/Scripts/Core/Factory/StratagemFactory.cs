using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class StratagemFactory : IStratagemFactory
{
    [Inject] private PlayerController _playerController;
    [Inject] private PlayerDataModel _playerData;
    [Inject] private WeaponController _weaponController;

    public Dictionary<StratagemDataSO, List<ActionNode>> Create(StratagemSlotSO slotSO)
    {
        Dictionary<StratagemDataSO, List<ActionNode>> dict = new();

        foreach (StratagemDataSO dataSO in slotSO.StratagemData)
        {
            List<ActionNode> node = new();
            foreach (StratagemActionSO actionSO in dataSO.Action)
            {
                ActionNode temp = new(actionSO);
                node.Add(temp);
            }

            dataSO.Context.Caster = _playerController;
            dataSO.Context.PlayerData = _playerData;
            dataSO.Context.WeaponSlot = _weaponController.GetWeaponSlot();

            node[0].Context = dataSO.Context;

            for (int i = 1; i < node.Count; i++)
            {
                node[i].Link(node[i - 1]);
            }

            dict.TryAdd(dataSO, node);
            Debug.Log($"{dataSO.Name} : 생성");
        }
        return dict;
    }
}
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class StratagemFactory : IStratagemFactory
{
    [Inject] private PlayerController _playerController;
    [Inject] private PlayerDataModel _playerData;
    [Inject] private WeaponController _weaponController;
    [Inject] private IPoolService _poolService;
    [Inject] private ISpawner _spawner;

    public Dictionary<StratagemDataSO, List<ActionNode>> Create(List<StratagemDataSO> slotSO)
    {
        Dictionary<StratagemDataSO, List<ActionNode>> dict = new();
        WeaponSlot weaponSlot = _weaponController.GetWeaponSlot();

        foreach (StratagemDataSO dataSO in slotSO)
        {
            List<ActionNode> node = new();
            foreach (StratagemActionSO actionSO in dataSO.Action)
            {
                ActionNode temp = new(actionSO);
                node.Add(temp);
            }

            ActionStruct actionStruct = new(_poolService, _spawner, _playerController, _playerData, weaponSlot, dataSO.Creation, dataSO.Damage);

            node[0].Struct = actionStruct;

            for (int i = 1; i < node.Count; i++)
            {
                node[i].Link(node[i - 1]);
            }

            dict.TryAdd(dataSO, node);
        }
        return dict;
    }
}
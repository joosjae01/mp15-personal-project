using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class StratagemPanel : MonoBehaviour
{
    [SerializeField] private PlayerStratagemSlotUI _prefab;
    [SerializeField] private Transform _root;

    private StratagemInputBuffer _buffer;
    private StratagemCoolDown _cooldown;
    private List<PlayerStratagemSlotUI> _slots = new();

    [Inject]
    public void Inject(StratagemInputBuffer buffer, StratagemCoolDown cooldown)
    {
        _buffer = buffer; 
        _cooldown = cooldown;
    }

    private void Start()
    {
        foreach (StratagemDataSO data in _buffer.Stratagems)
        {
            PlayerStratagemSlotUI slot = Instantiate(_prefab, _root);
            slot.Init(data, _cooldown);
            _slots.Add(slot);
        }

        _buffer.OnProgress += RefreshProgress;
        _buffer.OnDropped += RefreshDropped;
        _buffer.OnReset += RefreshReset;
    }

    private void OnDestroy()
    {
        _buffer.OnProgress -= RefreshProgress;
        _buffer.OnDropped -= RefreshDropped;
        _buffer.OnReset -= RefreshReset;
    }

    private void RefreshProgress(StratagemDataSO dataSO, int index)
    {
        foreach(PlayerStratagemSlotUI slot in _slots)
        {
            slot.OnProgress(dataSO, index);
        }
    }

    private void RefreshDropped(StratagemDataSO dataSO)
    {
        foreach(PlayerStratagemSlotUI slot in _slots)
        {
            slot.OnDropped(dataSO);
        }
    }

    private void RefreshReset()
    {
        foreach(PlayerStratagemSlotUI slot in _slots)
        {
            slot.OnReset();
        }
    }
}

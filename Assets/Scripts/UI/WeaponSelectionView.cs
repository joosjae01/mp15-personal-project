using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class WeaponSelectionView : MonoBehaviour, IView
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _scrollPrefab;
    [SerializeField] private List<Image> _weaponIcons;
    [SerializeField] private List<TextMeshProUGUI> _weaponNames;
    [SerializeField] private Sprite _null;
    
    private ReadyWeaponVM _vm;
    private IViewFactory _factory;
    private Dictionary<int, IView> _viewDict;

    [Inject]
    public void Inject(ReadyWeaponVM vm, WeaponSlotSO slotSO, IViewFactory factory)
    {
        _vm = vm;
        _factory = factory;
        SetDict(slotSO);
    }
    
    private void SetDelivery(bool isChanged)
    {
        for(int i = 0; i < _weaponIcons.Count; i++)
        {
            _weaponIcons[i].sprite = _null;
            _weaponNames[i].text = null;
        }

        List<WeaponContext> list = _vm.GetDelivery();
        for(int i = 0; i < list.Count; i++)
        {
            _weaponIcons[i].sprite = list[i].DataSO.Icon;
            _weaponNames[i].text = list[i].DataSO.Name;
        }
    }

    private void SetDict(WeaponSlotSO slotSO)
    {
        ViewContext context = new ViewContext(_scrollPrefab, _scrollRect.content, ViewType.WeaponSlot, slotSO.WeaponContext.Count);
        _viewDict = _factory.Create(context);
        foreach(KeyValuePair<int, IView> key in _viewDict)
        {
            key.Value.AddListener(SetDelivery);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        foreach (KeyValuePair<int, IView> key in _viewDict)
        {
            key.Value.RemoveListener(SetDelivery);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        SetDelivery(true);
    }

    public void AddListener(Action<bool> action)
    {
    }

    public void RemoveListener(Action<bool> action)
    {
    }
}

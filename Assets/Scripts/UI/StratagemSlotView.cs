using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class StratagemSlotView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;

    [Inject] private ReadyStratagemVM _vm;

    public ObservableProperty<bool> IsSelected;
    private StratagemDataSO _dataSO;

    public void Create(StratagemDataSO dataSO)
    {
        _dataSO = dataSO;
        _name.text = dataSO.Name;
        _icon.sprite = dataSO.Icon;
        IsSelected = new(false);
        _button.onClick.AddListener(Select);
    }

    public void Select()
    {
        if (!IsSelected.Value)
        {
            if (_vm.AddDataSO(_dataSO))
            {
                IsSelected.Value = true;
            }
        }
        else
        {
            if (_vm.RemoveDataSO(_dataSO))
            {
                IsSelected.Value = false;
            }
        }
    }

    public void Show()
    {
        _button.onClick.AddListener(Select);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _button.onClick.RemoveListener(Select);
        gameObject.SetActive(false);
    }

    public void AddListener(Action<bool> action)
    {
        IsSelected.AddListener(action);
    }

    public void RemoveListener(Action<bool> action)
    {
        IsSelected.RemoveListener(action);
    }
}

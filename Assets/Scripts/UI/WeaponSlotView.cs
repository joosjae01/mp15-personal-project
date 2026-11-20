using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class WeaponSlotView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private List<Image> _statusBar;
    [SerializeField] private Button _select;

    [Inject] private ReadyWeaponVM _vm;
    private int _index;
    public bool IsSelected;

    public void Create(int index)
    {
        _index = index;
        SetName();
        SetImage();
        SetStatus();
        IsSelected = false;
    }

    public void Select()
    {
        if (!IsSelected)
        {
            _vm.AddContext(_index);
            IsSelected = true;
        }
        else
        {
            _vm.RemoveContext(_index);
            IsSelected = true;
        }
        
    }

    private void SetName() => _name.text = _vm.GetName(_index);
    private void SetImage() => _icon.sprite = _vm.GetImage(_index);
    private void SetStatus()
    {
        _statusBar[0].fillAmount = _vm.GetPower(_index);
        _statusBar[1].fillAmount = _vm.GetFireRate(_index);
        _statusBar[2].fillAmount = _vm.GetStability(_index);
        _statusBar[3].fillAmount = _vm.GetMagazine(_index);
        _statusBar[4].fillAmount = _vm.GetRange(_index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _select.onClick.AddListener(Select);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _select.onClick.RemoveListener(Select);
    }
}

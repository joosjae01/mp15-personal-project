using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class WeaponSlotView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selected;
    [SerializeField] private List<Image> _statusBar;
    [SerializeField] private Button _select;
    [SerializeField] private SoundDataSO _sound;


    [Inject] private ISoundService _soundService;
    [Inject] private ReadyWeaponVM _vm;
    private int _index;
    public ObservableProperty<bool> IsSelected;

    public void Create(int index)
    {
        _index = index;
        SetName();
        SetImage();
        SetStatus();
        IsSelected = new(false);
        _select.onClick.AddListener(Select);
    }

    public void Select()
    {
        if (!IsSelected.Value)
        {
            _vm.AddContext(_index);
            IsSelected.Value = true;
            _selected.gameObject.SetActive(true);
            _soundService.PlaySFX(_sound);
        }
        else
        {
            _vm.RemoveContext(_index);
            IsSelected.Value = false;
            _selected.gameObject.SetActive(false);
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

    public void AddListener(Action<bool> action)
    {
        IsSelected.AddListener(action);
    }

    public void RemoveListener(Action<bool> action)
    {
        IsSelected.RemoveListener(action);
    }
}

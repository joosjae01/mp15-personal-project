using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PlayerWeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentName;
    [SerializeField] private TextMeshProUGUI _currentMagazine;
    [SerializeField] private TextMeshProUGUI _currentClip;
    [SerializeField] private TextMeshProUGUI _maxMagazine;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private List<Image> _weaponIndex;

    [Inject] private WeaponController _controller;
    private WeaponSlot _weaponSlot;
    private IWeapon _currentWeapon;
    
    private void Awake()
    {
        _weaponSlot = _controller.GetWeaponSlot();
        _currentWeapon = _weaponSlot.CurrentWeapon;
        ChangeWeapon(_currentWeapon);
    }

    private void OnEnable() => _weaponSlot.AddListener(ChangeWeapon);
    private void OnDisable() => _weaponSlot.RemoveListener(ChangeWeapon);

    private void RefreshName(string name) => _currentName.text = name;
    private void RefreshImage(Sprite image) => _weaponIcon.sprite = image;
    private void RefreshIndex(int index)
    {
        for (int i = 0; i < _weaponIndex.Count; i++)
        {
            if (i == index) _weaponIndex[i].gameObject.SetActive(true);
            else _weaponIndex[i].gameObject.SetActive(false);
        }
    }
    private void RefreshClip(int clips) => _currentClip.text = clips.ToString();
    private void RefreshMagazine(int currentMagazine) => _currentMagazine.text = currentMagazine.ToString();
    private void RefreshMaxMagazine(int maxMagazine) => _maxMagazine.text = maxMagazine.ToString();
    
    public void ChangeWeapon(IWeapon weapon)
    {
        UnBindWeaponEvents();
        BindWeaponEvents();
    }

    public void BindWeaponEvents()
    {
        _currentWeapon = _weaponSlot.CurrentWeapon;
        _currentWeapon.Model.CurrentMagazine.AddListener(RefreshMagazine);
        _currentWeapon.Model.MaxMagazine.AddListener(RefreshMaxMagazine);
        _currentWeapon.Model.AmmoClip.AddListener(RefreshClip);

        RefreshName(_currentWeapon.Model.Name);
        RefreshClip(_currentWeapon.Model.AmmoClip.Value);
        RefreshImage(_currentWeapon.Model.Icon);
        RefreshIndex(_weaponSlot.GetIndex());
        RefreshMagazine(_currentWeapon.Model.CurrentMagazine.Value);
        RefreshMaxMagazine(_currentWeapon.Model.MaxMagazine.Value);
    }

    public void UnBindWeaponEvents()
    {
        _currentWeapon.Model.CurrentMagazine.RemoveListener(RefreshMagazine);
        _currentWeapon.Model.MaxMagazine.RemoveListener(RefreshMaxMagazine);
        _currentWeapon.Model.AmmoClip.RemoveListener(RefreshClip);
    }
}

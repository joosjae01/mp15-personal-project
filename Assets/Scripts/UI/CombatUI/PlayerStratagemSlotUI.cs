using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerStratagemSlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _activeImage;
    [SerializeField] private GameObject _deactiveImage;
    [SerializeField] private GameObject _cooldownImage;
    [SerializeField] private TextMeshProUGUI _cooldownText;
    [SerializeField] private Transform _arrowRoot;
    [SerializeField] private CommandArrow _arrowPrefab;

    private List<CommandArrow> _arrows = new();
    private StratagemCoolDown _cooldown;
    private StratagemDataSO _dataSO;

    private bool _isDropped;

    private void Update() => Refresh();

    private void Refresh()
    {
        float remain = _cooldown.GetRemain(_dataSO);
        bool onCooldown = remain > 0.0f;

        _cooldownImage.SetActive(onCooldown);
        _deactiveImage.SetActive(!onCooldown && _isDropped);
        _activeImage.SetActive(!onCooldown && !_isDropped);

        _cooldownText.text = onCooldown ? $"{remain:F1}s" : "준비";
    }
    
    public void Init(StratagemDataSO dataSO, StratagemCoolDown cooldown)
    {
        _dataSO = dataSO;
        _cooldown = cooldown;
        _icon.sprite = dataSO.Icon;
        _name.text = dataSO.Name;

        foreach(Command cmd in dataSO.Command)
        {
            CommandArrow arrow = Instantiate(_arrowPrefab, _arrowRoot);
            arrow.SetDirection(cmd);
            _arrows.Add(arrow);
        }
    }

    public void SetProgress(int index)
    {
        for(int i = 0; i < _arrows.Count; i++)
        {
            _arrows[i].SetActive(i < index);
        }
    }

    public void SetDrop(bool isDropped)
    {
        _deactiveImage.SetActive(isDropped);
    }

    public void SetCoolDown(float elapsed)
    {
        bool isCoolOn = elapsed <= 0.0f;
        _cooldownText.text = isCoolOn ? "준비" : $"{elapsed:F1}s";
    }

    public void OnProgress(StratagemDataSO dataSO, int index)
    {
        if (_dataSO != dataSO) return;
        SetProgress(index);
    }

    public void OnDropped(StratagemDataSO dataSO)
    {
        if (_dataSO != dataSO) return;
        _isDropped = true;
        SetProgress(0);
    }

    public void OnReset()
    {
        _isDropped = false;
        SetProgress(0);
    }

    public void UnBindEvents()
    {

    }
}

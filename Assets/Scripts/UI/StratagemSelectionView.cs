using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class StratagemSelectionView : MonoBehaviour, IView
{
    [SerializeField] private ScrollRect _attackRect;
    [SerializeField] private ScrollRect _defenseRect;
    [SerializeField] private ScrollRect _supplyRect;
    [SerializeField] private ScrollRect _specialRect;

    [SerializeField] private GameObject _scrollPrefab;
    [SerializeField] private List<Image> _stratagemIcons;
    [SerializeField] private List<TextMeshProUGUI> _stratagemNames;
    [SerializeField] private List<TextMeshProUGUI> _stratagemDescs;
    [SerializeField] private Sprite _null;
    [SerializeField] private Button _confirm;
    [SerializeField] private SoundDataSO _sfx;
    private IObjectResolver _resolver;
    private ISoundService _soundService;
    private ReadyStratagemVM _vm;
    private StratagemSlotSO _slotSO;
    private Dictionary<StratagemDataSO, IView> _viewDict = new();
    private bool _isCreated;

    [Inject]
    public void Inject(ReadyStratagemVM vm, StratagemSlotSO slotSO, IObjectResolver resolver, ISoundService soundService)
    {
        _vm = vm;
        _resolver = resolver;
        _slotSO = slotSO;
        _soundService = soundService;
    }

    private void PlaySound()
    {
        _soundService.PlaySFX(_sfx);
    }

    private void SetDelivery(bool isChanged)
    {
        for (int i = 0; i < _stratagemIcons.Count; i++)
        {
            _stratagemIcons[i].sprite = _null;
            _stratagemNames[i].text = "미선택";
            _stratagemDescs[i].text = null;
        }

        List<StratagemDataSO> list = _vm.GetDelivery();
        for (int i = 0; i < list.Count; i++)
        {
            _stratagemIcons[i].sprite = list[i].Icon;
            _stratagemNames[i].text = list[i].Name;
            _stratagemDescs[i].text = list[i].Desc;
        }
    }

    private void SetDict(StratagemSlotSO slotSO)
    {
        
        foreach(StratagemDataSO dataSO in slotSO.StratagemData)
        {
            ScrollRect tempRect = dataSO.Type switch
            {
                StratagemType.Attack => _attackRect,
                StratagemType.Defense => _defenseRect,
                StratagemType.Supply => _supplyRect,
                StratagemType.Special => _specialRect,
                _ => _specialRect,
            };
            
            ViewContext context = new ViewContext(_scrollPrefab, tempRect.content, ViewType.StratagemSlot, slotSO.StratagemData.Count);
            GameObject temp = _resolver.Instantiate(_scrollPrefab, tempRect.content);
            temp.GetComponent<StratagemSlotView>().Create(dataSO);
            _viewDict[dataSO] = temp.GetComponent<IView>();
        }
        
        foreach (KeyValuePair<StratagemDataSO, IView> key in _viewDict)
        {
            key.Value.AddListener(SetDelivery);
        }
    }

    public void Hide()
    {
        _confirm.onClick.RemoveListener(Hide);
        _confirm.onClick.RemoveListener(PlaySound);
        gameObject.SetActive(false);

        foreach (KeyValuePair<StratagemDataSO, IView> key in _viewDict)
        {
            key.Value.RemoveListener(SetDelivery);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        SetDelivery(true);

        _confirm.onClick.RemoveListener(Hide);
        _confirm.onClick.AddListener(Hide);
        _confirm.onClick.AddListener(PlaySound);

        if (!_isCreated)
        {
            SetDict(_slotSO);
            _isCreated = true;
        }
        else
        {
            foreach(KeyValuePair<StratagemDataSO, IView> pair in _viewDict)
            {
                pair.Value.AddListener(SetDelivery);
            }
        }
        
    }

    public void AddListener(Action<bool> action)
    {
    }

    public void RemoveListener(Action<bool> action)
    {
    }
}

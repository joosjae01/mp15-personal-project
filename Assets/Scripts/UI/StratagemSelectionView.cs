    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VContainer;

    public class StratagemSelectionView : MonoBehaviour, IView
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _scrollPrefab;
        [SerializeField] private List<Image> _stratagemIcons;
        [SerializeField] private List<TextMeshProUGUI> _stratagemNames;
    
        private ReadyStratagemVM _vm;
        private IViewFactory _factory;
        private Dictionary<int, IView> _viewDict;

        [Inject]
        public void Inject(ReadyStratagemVM vm, StratagemSlotSO slotSO, IViewFactory factory)
        {
            _vm = vm;
            _factory = factory;
            SetDict(slotSO);
        }
    
        private void SetDelivery(bool isChanged)
        {
            for(int i = 0; i < _stratagemIcons.Count; i++)
            {
                _stratagemIcons[i].sprite = null;
                _stratagemNames[i].text = null;
            }

            List<StratagemDataSO> list = _vm.GetDelivery();
            for(int i = 0; i < list.Count; i++)
            {
                _stratagemIcons[i].sprite = list[i].Icon;
                _stratagemNames[i].text = list[i].Name;
            }
        }

        private void SetDict(StratagemSlotSO slotSO)
        {
            ViewContext context = new ViewContext(_scrollPrefab, _scrollRect.content, ViewType.StratagemSlot, slotSO.StratagemData.Count);
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

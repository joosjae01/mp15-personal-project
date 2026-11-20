using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class StratagemSlotView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private TextMeshProUGUI _coolDown;
    [SerializeField] private Button _button;

    [Inject] private ReadyStratagemVM _vm;
    private int _index;
    public bool IsSelected;

    public void Create(int index)
    {
        _index = index;
        SetName();
        SetImage();
        SetDesc();
        SetCoolDown();
        IsSelected = false;
    }

    public void Select()
    {
        if (!IsSelected)
        {
            _vm.AddDataSO(_index);
            IsSelected = true;
        }
        else
        {
            _vm.RemoveDataSO(_index);
            IsSelected = false;
        }
    }

    private void SetName()
    {
        _name.text = _vm.GetName(_index);
    }

    private void SetImage()
    {
        _icon.sprite = _vm.GetImage(_index);
    }

    private void SetDesc()
    {
        _desc.text = _vm.GetDesc(_index);
    }

    private void SetCoolDown()
    {
        _coolDown.text = "CoolDown : " + _vm.GetCoolDown(_index).ToString();
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
}

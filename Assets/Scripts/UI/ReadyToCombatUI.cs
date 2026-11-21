using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class ReadyToCombatUI : MonoBehaviour, IView
{
    [SerializeField] private List<Image> _weaponList;
    [SerializeField] private List<TextMeshProUGUI> _weaponNameList;
    [SerializeField] private List<Image> _gemList;
    [SerializeField] private List<TextMeshProUGUI> _gemNameList;
    [SerializeField] private List<TextMeshProUGUI> _targetNameList;
    [SerializeField] private List<TextMeshProUGUI> _targetDescList;
    [SerializeField] private Button _start;
    [SerializeField] private Button _quit;

    [SerializeField] private Sprite _nullWeapon;
    [SerializeField] private Sprite _nullGem;

    private ReadyWeaponVM _weaponVM;
    private ReadyStratagemVM _stratagemVM;
    private ReadyMissionVM _missionVM;
    private MissionBoxSO _boxSO;

    [Inject]
    public void Inject(ReadyWeaponVM weaponVM, ReadyStratagemVM stratagemVM, ReadyMissionVM missionVM, MissionBoxSO boxSO)
    {
        _weaponVM = weaponVM;
        _stratagemVM = stratagemVM;
        _missionVM = missionVM;
        _boxSO = boxSO;
    }

    private void Init()
    {
        for(int i = 0; i  < _weaponList.Count; i++)
        {
            _weaponNameList[i].text = "미선택";
            _weaponList[i].sprite = _nullWeapon;
            _gemNameList[i].text = "미선택";
            _gemList[i].sprite = _nullGem;
        }

        List<WeaponContext> weapons = _weaponVM.GetDelivery();
        List<StratagemDataSO> gems = _stratagemVM.GetDelivery();
        List<TargetContext> targets = _missionVM.Roll();

        for(int i = 0; i < weapons.Count; i++)
        {
            _weaponList[i].sprite = weapons[i].DataSO.Icon;
            _weaponNameList[i].text = weapons[i].DataSO.Name;
        }

        for(int i = 0;i < gems.Count; i++)
        {
            _gemList[i].sprite = gems[i].Icon;
            _gemNameList[i].text = gems[i].Name;
        }

        for (int i = 0; i < targets.Count; i++)
        {
            _targetNameList[i].text = targets[i].Name;
            _targetDescList[i].text = targets[i].Desc;
        }

        _start.onClick.AddListener(StartMission);
        _quit.onClick.AddListener(DeActive);
    }

    private void StartMission()
    {
        if (_weaponVM.GetDelivery().Count == 0) return;
        if (_stratagemVM.GetDelivery().Count == 0) return;

        _boxSO.Weapons = _weaponVM.GetDelivery();
        _boxSO.Staratgems = _stratagemVM.GetDelivery();

        SceneManager.LoadScene("GameScene");
    }

    private void DeActive()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _start.onClick.AddListener(StartMission);
        _quit.onClick.AddListener(DeActive);
        Init();
    }

    public void Hide()
    {
        _start.onClick.RemoveListener(StartMission);
        _quit.onClick.RemoveListener(DeActive);
        gameObject.SetActive(false);
    }

    public void AddListener(Action<bool> action)
    {
        
    }

    public void RemoveListener(Action<bool> action)
    {
    }
}

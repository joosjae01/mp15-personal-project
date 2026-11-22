using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class GameResultPanel : MonoBehaviour, IView
{
    [SerializeField] private Image _completed;
    [SerializeField] private Image _failed;

    [SerializeField] private TextMeshProUGUI _killCount;
    [SerializeField] private TextMeshProUGUI _elapsedTime;
    [SerializeField] private TextMeshProUGUI _completedTargets;

    [SerializeField] private Button _goToLobby;

    [SerializeField] private GameObject _combatUI;

    private MissionController _mission;
    private MissionDataModel _model;
    private InputManager _input;
    private IPauseService _pause;
    private ISpawner _spawner;

    [Inject]
    public void Inject(MissionController mission, ISpawner spawner, IPauseService pause, InputManager input)
    {
        _input = input;
        _pause = pause;
        _mission = mission;
        _spawner = spawner;
        _model = mission.DataModel;
    }

    private void Start() => Bind();
    private void OnDestroy() => UnBind();

    private void Bind()
    {
        _model.IsCompleted.AddListener(SetComplete);
        _model.IsFailed.AddListener(SetFail);
        Hide();
    }

    private void SetComplete(bool isCompleted)
    {
        _completed.gameObject.SetActive(true);
        _failed.gameObject.SetActive(false);

        _killCount.text = _spawner.KillCount.ToString();
        _elapsedTime.text = $"{(int)_mission.Elapsed / 60 : 00} : {(int)_mission.Elapsed % 60 : 00}";
        _completedTargets.text = $"{_mission.GetMissionCount()} / 3";
        _goToLobby.onClick.AddListener(SwitchScene);
        _goToLobby.onClick.AddListener(_pause.ResumeGame);
        _goToLobby.onClick.AddListener(() => _input.SetPaused(false));

        Show();
    }

    private void SetFail(bool isFailed)
    {
        _completed.gameObject.SetActive(false);
        _failed.gameObject.SetActive(true);

        _killCount.text = _spawner.KillCount.ToString();
        _elapsedTime.text = $"{(int)_mission.Elapsed / 60: 00} : {(int)_mission.Elapsed % 60: 00}";
        _completedTargets.text = $"{_mission.GetMissionCount()} / 3";
        _goToLobby.onClick.AddListener(SwitchScene);
        _goToLobby.onClick.AddListener(_pause.ResumeGame);
        _goToLobby.onClick.AddListener(() => _input.SetPaused(false));

        Show();
    }

    private void UnBind()
    {
        _goToLobby.onClick.RemoveAllListeners();
        _model.IsCompleted.RemoveListener(SetComplete);
        _model.IsFailed.RemoveListener(SetFail);
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("ReadyPlace");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _combatUI.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _combatUI.SetActive(true);
    }

    public void AddListener(Action<bool> action)
    {

    }

    public void RemoveListener(Action<bool> action)
    {
        
    }
}

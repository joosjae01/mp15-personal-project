using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _resume;
    [SerializeField] private Button _goToMain;

    IPauseService _pause;
    InputManager _inputManager;
    PlayerInputModel _inputModel;

    [Inject]
    public void Inject(IPauseService pause, InputManager inputManager, PlayerInputModel inputModel)
    {
        _inputModel = inputModel;
        _inputManager = inputManager;
        _pause = pause;
    }

    private void Start()
    {
        _inputModel.OnEscape.AddListener(Show);
        _resume.onClick.AddListener(Resume);
        _goToMain.onClick.AddListener(GoToMain);
        Hide();
    }

    private void OnDestroy()
    {
        _inputModel.OnEscape.RemoveListener(Show);
        _resume.onClick.RemoveListener(Resume);
        _goToMain.onClick.RemoveListener(GoToMain);
    }

    private void Resume()
    {
        _pause.ResumeGame();
        _inputManager.SetPaused(false);
        Hide();
    }

    private void GoToMain()
    {
        Resume();
        SceneManager.LoadScene("Title");
    }

    private void Show(bool OnEscape)
    {
        _pause.PauseGame();
        _inputManager.SetPaused(true);
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

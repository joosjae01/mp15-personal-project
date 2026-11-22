using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _quit;

    private void Start()
    {
        _start.onClick.AddListener(StartGame);
        _quit.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        _start.onClick.RemoveListener(StartGame);
        _quit.onClick.RemoveListener(QuitGame);
    }

    private void StartGame() => SceneManager.LoadScene("ReadyPlace");
    private void QuitGame() => Application.Quit();
}

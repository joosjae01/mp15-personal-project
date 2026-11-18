using UnityEngine;

public class PauseService : IPauseService
{
    public bool IsPaused { get; private set; }

    public void PauseGame()
    {
        if (IsPaused) return;
        Time.timeScale = 0.0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;
        Time.timeScale = 1.0f;
        IsPaused = false;
    }
}

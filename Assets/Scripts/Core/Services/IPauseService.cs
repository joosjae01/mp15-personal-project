using UnityEngine;

public interface IPauseService
{
    public bool IsPaused { get; }

    public void PauseGame();
    public void ResumeGame();
}

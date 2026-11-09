using UnityEngine;

public interface ISoundService
{
    public void PlaySound(SoundEffectType type);
    public void PauseSound(SoundEffectType type);
    public void ResumeSound(SoundEffectType type);
}

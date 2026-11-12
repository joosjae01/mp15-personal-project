using UnityEngine;

public class MockSoundManager : MonoBehaviour, ISoundService
{
    public void PauseSound(SoundEffectType type)
    {
        Debug.Log($"{name} : {SoundEffectType.PlayerWalking} 재생 종료");
    }

    public void PlaySound(SoundEffectType audioClip)
    {
        Debug.Log($"{name} : {SoundEffectType.PlayerWalking} 재생 중");
    }

    public void ResumeSound(SoundEffectType type)
    {
        Debug.Log($"{name} : {SoundEffectType.PlayerWalking} 다시 재생 중");
    }
}

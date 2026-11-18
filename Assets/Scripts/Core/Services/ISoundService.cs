using UnityEngine;

public interface ISoundService
{
    public void PlaySFX(SoundDataSO dataSO);
    public void PlayBGM(SoundDataSO dataSO);
    public void StopBGM();
}

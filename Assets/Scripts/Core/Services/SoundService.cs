using UnityEngine;

public class SoundService : MonoBehaviour, ISoundService
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private float _sfxVolume;
    [SerializeField] private float _bgmVolume;

    public void PlayBGM(SoundDataSO dataSO)
    {
        if (!dataSO.IsBGM) return;
        if (_bgmSource.isPlaying && _bgmSource.clip == dataSO.Clip) return;
        _bgmSource.clip = dataSO.Clip;
        _bgmSource.volume = _bgmVolume;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Pause();
    }

    public void PlaySFX(SoundDataSO dataSO)
    {
        if (dataSO.IsBGM) return;
        _audioSource.PlayOneShot(dataSO.Clip, _sfxVolume);
    }

    public void SetBGMVolume(float volume) => _bgmVolume = volume;
    public void SetSFXVolume(float volume) => _sfxVolume = volume;
}

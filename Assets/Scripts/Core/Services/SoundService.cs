using UnityEngine;

public class SoundService : ISoundService
{
    private AudioSource _audioSource;
    private AudioSource _bgmSource;
    private float _sfxVolume;
    private float _bgmVolume;

    public SoundService()
    {
        _audioSource = new();
        _bgmSource = new();
        _sfxVolume = 0.5f;
        _bgmVolume = 0.5f;
    }

    public void PlayBGM(SoundDataSO dataSO)
    {
        if (!dataSO.IsBGM || _bgmSource.isPlaying) return;
        _bgmSource.clip = dataSO.Clip;
        _bgmSource.volume = _bgmVolume;
        _bgmSource.Play();
    }

    public void StopBGM(SoundDataSO dataSO)
    {
        if (!dataSO.IsBGM || !_bgmSource.isPlaying) return;
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

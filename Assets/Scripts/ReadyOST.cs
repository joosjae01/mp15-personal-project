using UnityEngine;
using VContainer;

public class ReadyOST : MonoBehaviour
{
    [SerializeField] SoundDataSO _bgm;
    private ISoundService _soundService;

    [Inject]
    public void Inject(ISoundService soundService)
    {
        _soundService = soundService;
    }

    private void Start()
    {
        _soundService.PlayBGM(_bgm);
    }

    private void OnDestroy()
    {
        _soundService.StopBGM();
    }
}

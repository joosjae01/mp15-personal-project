using UnityEngine;

[CreateAssetMenu(fileName = "SFX_SoundData", menuName = "SO/Core/SoundData")]
public class SoundDataSO : ScriptableObject
{
    [field : SerializeField] public bool IsBGM { get; private set; }
    public AudioClip Clip;
}

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _bgmAudioSource;
    [SerializeField] AudioSource _seAudioSource;

    [SerializeField] List<BGMSoundData> _bgmSoundData;
    [SerializeField] List<SESoundData> _seSoundData;

    public float masterVolume = 1;
    public float bgmMasterVolume = 1;
    public float seMasterVolume = 1;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = _bgmSoundData.Find(data => data.bgm == bgm);
        _bgmAudioSource.clip = data.audioClip;
        _bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        _bgmAudioSource.Play();
    }


    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = _seSoundData.Find(data => data.se == se);
        _seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        _seAudioSource.PlayOneShot(data.audioClip);
    }

}

[System.Serializable]
public class BGMSoundData
{
    // ‚±‚ê‚ªƒ‰ƒxƒ‹‚É‚È‚é
    public enum BGM
    {
        Title,
        InGame,
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    // ‚±‚ê‚ªƒ‰ƒxƒ‹‚É‚È‚é
    public enum SE
    {
        Attack,
        Damage,
    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}
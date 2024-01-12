using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _bgmAudioSource;
    [SerializeField] AudioSource _seAudioSource;

    [SerializeField] List<BGMSoundData> _bgmSoundData;
    [SerializeField] List<SESoundData> _seSoundData;

    [SerializeField] float _masterVolume = 1;
    [SerializeField] float _bgmMasterVolume = 1;
    [SerializeField] float _seMasterVolume = 1;

    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _seSlider;

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

    private void Start()
    {
        //�X���C�_�[�𓮂��������̏�����o�^�BStart�łȂ��Ƃ��܂������Ȃ��B
        _masterSlider.onValueChanged.AddListener(SetMasterVolume);
        _bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        _seSlider.onValueChanged.AddListener(SetSEVolume);
    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = _bgmSoundData.Find(data => data.bgm == bgm);
        _bgmAudioSource.clip = data.audioClip;
        _bgmAudioSource.volume = data.volume * _bgmMasterVolume * _masterVolume;
        _bgmAudioSource.Play();
    }


    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = _seSoundData.Find(data => data.se == se);
        _seAudioSource.volume = data.volume * _seMasterVolume * _masterVolume;
        _seAudioSource.PlayOneShot(data.audioClip);
    }

    /// <summary>
    /// Master�̉��ʂ��Z�b�g���܂�
    /// value��Slider�̏����ݒ�ł���0�`1�̒l��z�肵�Ă��܂��B
    /// </summary>
    /// <param name="value">Master�̉���</param>
    public void SetMasterVolume(float value) => _masterVolume = Mathf.Clamp01(value);

    /// <summary>
    /// BGM�̉��ʂ��Z�b�g���܂�
    /// value��Slider�̏����ݒ�ł���0�`1�̒l��z�肵�Ă��܂��B
    /// </summary>
    /// <param name="value">BGM�̉���</param>
    public void SetBGMVolume(float value) => _bgmMasterVolume = Mathf.Clamp01(value);

    /// <summary>
    /// SE�̉��ʂ��Z�b�g���܂�
    /// value��Slider�̏����ݒ�ł���0�`1�̒l��z�肵�Ă��܂��B
    /// </summary>
    /// <param name="value">SE�̉���</param>
    public void SetSEVolume(float value) => _seMasterVolume = Mathf.Clamp01(value);
}

[System.Serializable]
public class BGMSoundData
{
    // ���ꂪ���x���ɂȂ�
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
    // ���ꂪ���x���ɂȂ�
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
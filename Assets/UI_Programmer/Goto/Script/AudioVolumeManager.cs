using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _seSlider;

    void Start()
    {
        //�X���C�_�[�𓮂��������̏�����o�^�BStart�łȂ��Ƃ��܂������Ȃ��B
        _masterSlider.onValueChanged.AddListener(SetAudioMixerMasterVolume);
        _bgmSlider.onValueChanged.AddListener(SetAudioMixerBGMVolume);
        _seSlider.onValueChanged.AddListener(SetAudioMixerSEVolume);
    }

    /// <summary>Master�̉��ʂ��Z�b�g���܂�</summary>
    /// <param name="value">Master�̉���</param>
    public void SetAudioMixerMasterVolume(float value)
    {
        // value��Slider�̏����ݒ�ł���0�`1�̒l��z�肵�Ă��܂��B
        value = Mathf.Clamp01(value);
        // �f�V�x�����l�������v�Z
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        _audioMixer.SetFloat("MasterVolume", decibel);
    }

    /// <summary>BGM�̉��ʂ��Z�b�g���܂�</summary>
    /// <param name="value">BGM�̉���</param>
    public void SetAudioMixerBGMVolume(float value)
    {
        // value��Slider�̏����ݒ�ł���0�`1�̒l��z�肵�Ă��܂��B
        value = Mathf.Clamp01(value);
        // �f�V�x�����l�������v�Z
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        _audioMixer.SetFloat("BGMVolume", decibel);
    }

    /// <summary>SE�̉��ʂ��Z�b�g���܂�</summary>
    /// <param name="value">SE�̉���</param>
    public void SetAudioMixerSEVolume(float value)
    {
        // value��Slider�̏����ݒ�ł���0�`1�̒l��z�肵�Ă��܂��B
        value = Mathf.Clamp01(value);
        // �f�V�x�����l�������v�Z
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        _audioMixer.SetFloat("SEVolume", decibel);
    }
}

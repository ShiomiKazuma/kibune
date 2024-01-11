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
        //スライダーを動かした時の処理を登録。Startでないとうまくいかない。
        _masterSlider.onValueChanged.AddListener(SetAudioMixerMasterVolume);
        _bgmSlider.onValueChanged.AddListener(SetAudioMixerBGMVolume);
        _seSlider.onValueChanged.AddListener(SetAudioMixerSEVolume);
    }

    /// <summary>Masterの音量をセットします</summary>
    /// <param name="value">Masterの音量</param>
    public void SetAudioMixerMasterVolume(float value)
    {
        // valueはSliderの初期設定である0～1の値を想定しています。
        value = Mathf.Clamp01(value);
        // デシベルを考慮した計算
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        _audioMixer.SetFloat("MasterVolume", decibel);
    }

    /// <summary>BGMの音量をセットします</summary>
    /// <param name="value">BGMの音量</param>
    public void SetAudioMixerBGMVolume(float value)
    {
        // valueはSliderの初期設定である0～1の値を想定しています。
        value = Mathf.Clamp01(value);
        // デシベルを考慮した計算
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        _audioMixer.SetFloat("BGMVolume", decibel);
    }

    /// <summary>SEの音量をセットします</summary>
    /// <param name="value">SEの音量</param>
    public void SetAudioMixerSEVolume(float value)
    {
        // valueはSliderの初期設定である0～1の値を想定しています。
        value = Mathf.Clamp01(value);
        // デシベルを考慮した計算
        float decibel = 20f * Mathf.Log10(value);
        decibel = Mathf.Clamp(decibel, -80f, 0f);
        _audioMixer.SetFloat("SEVolume", decibel);
    }
}

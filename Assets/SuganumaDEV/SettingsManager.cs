using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ResolutionPreset

public enum ResolutionPreset
{
    // 16 : 9
    FHD,    // 1920 x 1080
    WQHD,   // 2560 x 1440

    // 16 : 10
    WUXGA,  // 1920 x 1200
    WQXGA,  // 2560 x 1600
}

#endregion

#region RefreshRatePreset

public enum RefreshRatePreset
{
    _60,    // 60
    _75,    // 75
    _120,   // 120
    _144,   // 144
    _165,   // 165
}

#endregion

public class SettingsManager : MonoBehaviour
{
    /// <summary> 解像度とリフレッシュレートを設定する </summary>
    /// <param name="resolution"></param>
    public void SetResolution(ResolutionPreset resolution, RefreshRatePreset rate, FullScreenMode fullScreenMode)
    {
        int width, height, refreshRate;

        width = 1920;
        height = 1080;
        refreshRate = 60;

        switch (resolution)
        {
            case ResolutionPreset.FHD:
                width = 1920;
                height = 1080;
                break;
            case ResolutionPreset.WQHD:
                width = 2560;
                height = 1440;
                break;
            case ResolutionPreset.WUXGA:
                width = 1920;
                height = 1200;
                break;
            case ResolutionPreset.WQXGA:
                width = 2560;
                height = 1600;
                break;
        }

        switch (rate)
        {
            case RefreshRatePreset._60:
                refreshRate = 60;
                break;
            case RefreshRatePreset._75:
                refreshRate = 75;
                break;
            case RefreshRatePreset._120:
                refreshRate = 120;
                break;
            case RefreshRatePreset._144:
                refreshRate = 144;
                break;
            case RefreshRatePreset._165:
                refreshRate = 165;
                break;
        }

        Screen.SetResolution(width, height, fullScreenMode);
        Application.targetFrameRate = refreshRate;
    }

    public void GetResolution()
    {
        Debug.Log($"{Screen.currentResolution.ToString()}");
    }
}

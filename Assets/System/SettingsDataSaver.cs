using SLib.Singleton;
using SLib.UI;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Auth suganuma

public class SettingsDataSaver : SingletonBaseClass<SettingsDataSaver>
{
    [SerializeField]
    Dropdown _resolutionDD;
    [SerializeField]
    Dropdown _refreshRateDD;
    [SerializeField]
    Dropdown _displayDD;
    [SerializeField]
    Slider _masterVol;
    [SerializeField]
    Slider _bgmVol;
    [SerializeField]
    Slider _seVol;

    string _settingsDataPath = Application.dataPath + "/SettingsSavedData.json";

    protected override void ToDoAtAwakeSingleton()
    {

    }

    public void SaveSettingsData()  // 設定項目ウィンドウが閉じられたときこれを呼び出す
    {
        SettingsDataTemplate _settingsDataTemplate = new();

        _settingsDataTemplate._volMaster = _masterVol.value;
        _settingsDataTemplate._volBGM = _bgmVol.value;
        _settingsDataTemplate._volSE = _seVol.value;
        _settingsDataTemplate._resolutionIndex = _resolutionDD.value;
        _settingsDataTemplate._refreshRateIndex = _refreshRateDD.value;
        _settingsDataTemplate._displayIndex = _displayDD.value;

        string jsonStr = JsonUtility.ToJson(_settingsDataTemplate);

        StreamWriter sw = new StreamWriter(_settingsDataPath, false);
        print(jsonStr);
        sw.WriteLine(jsonStr);
        sw.Flush();
        sw.Close();

#if UNITY_EDITOR
        AssetDatabase.Refresh();    // これで変更が即時に可視化できそう
#endif
    }
}

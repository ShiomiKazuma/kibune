using SLib.Singleton;
using SLib.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Auth suganuma
public class SettingsDataSaver : SingletonBaseClass<SettingsDataSaver>
{
    DisplaySettingsManager _settingMan;
    SettingsDataTemplate _settingsDataTemplate = new();

    Slider _masterVol, _bgmVol, _seVol;



    string _settingsDataPath = Application.dataPath + "/SettingsSavedData.json";

    protected override void ToDoAtAwakeSingleton()
    {
        _settingMan = GameObject.FindAnyObjectByType<DisplaySettingsManager>();
    }

    public void SavePlayerData()
    {
        string jsonStr = JsonUtility.ToJson(_settingsDataTemplate);

        StreamWriter sw = new StreamWriter(_settingsDataPath, false);
        sw.WriteLine(jsonStr);
        sw.Flush();
        sw.Close();
    }

    public void SaveSettingsData()  // Settings Window ‚Ì Exit ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½‚Æ‚«
    {

    }
}

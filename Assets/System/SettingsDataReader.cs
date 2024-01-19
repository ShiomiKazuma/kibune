using SLib.Singleton;
using SLib.Systems;
using SLib.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// auth suganuma
public class SettingsDataReader : SingletonBaseClass<SettingsDataReader>
{
    string _settingsDataPath = Application.dataPath + "/SettingsSavedData.json";

    protected override void ToDoAtAwakeSingleton()
    {

    }
}

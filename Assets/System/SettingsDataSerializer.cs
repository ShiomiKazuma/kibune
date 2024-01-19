using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsDataSerializer : MonoBehaviour
{
    public SettingsDataTemplate ReadSaveData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/SettingsSavedData.json");
        string dataStr = sr.ReadToEnd();
        sr.Close();
        return JsonUtility.FromJson<SettingsDataTemplate>(dataStr);
    }
}

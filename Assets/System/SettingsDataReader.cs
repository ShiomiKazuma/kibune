using SLib.Singleton;
using SLib.Systems;
using SLib.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// auth suganuma
public class SettingsDataReader : SingletonBaseClass<SettingsDataReader>
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

    SettingsDataSerializer _serializer;

    string _settingsDataPath = Application.dataPath + "/SettingsSavedData.json";

    protected override void ToDoAtAwakeSingleton()
    {
        _serializer = GameObject.FindObjectOfType<SettingsDataSerializer>();

        try
        {
            var datas = _serializer.ReadSaveData(); // Ç±Ç±Ç™ó·äOî≠ê∂åπ

            _resolutionDD.value = datas._resolutionIndex;
            _refreshRateDD.value = datas._refreshRateIndex;
            _displayDD.value = datas._displayIndex;

            _masterVol.value = datas._volMaster;
            _bgmVol.value = datas._volBGM;
            _seVol.value = datas._volSE;
        }
        catch (FileNotFoundException)
        {
            _resolutionDD.value = 0;
            _refreshRateDD.value = 0;
            _displayDD.value = 0;

            _masterVol.value = .5f;
            _bgmVol.value = .5f;
            _seVol.value = .5f;
        }
    }
}

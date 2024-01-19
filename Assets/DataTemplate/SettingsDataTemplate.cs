using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auth suganuma
[Serializable]
public class SettingsDataTemplate
{
    public float _volMaster;       // on value changed
    public float _volBGM;
    public float _volSE;
    public int _resolutionIndex;       // on drop down value changed
    public int _refreshRateIndex;
    public int _displayIndex;
}

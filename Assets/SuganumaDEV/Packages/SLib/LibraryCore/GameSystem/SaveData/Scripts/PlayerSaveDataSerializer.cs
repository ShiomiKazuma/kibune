using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
// 作成 菅沼
public class PlayerSaveDataSerializer : SingletonBaseClass<PlayerSaveDataSerializer> // セーブデータの展開
{
    protected override void ToDoAtAwakeSingleton() { }

    public SaveDataTemplate ReadSaveData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/PlayerSavedData.json");
        string dataStr = sr.ReadToEnd();
        sr.Close();
        return JsonUtility.FromJson<SaveDataTemplate>(dataStr);
    }
}

using SLib.Singleton;
using SLib.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class FramedInGameEventProgressData
{
    /// #インベントリインデックス#
    /// 0 - 監視カメラ
    /// 1 - 凶器のナイフ
    /// 2 - 証拠の捏造前の写真
    /// 

    public List<bool> Finished;
}

public class FramedEventsInGameGeneralManager : SingletonBaseClass<FramedEventsInGameGeneralManager>
{
    /// ＃フロー＃
    /// 2．監視カメラをもぐ → 友人宅へ（インベントリ追加）
    /// 3．並行して凶器を探しに（インベントリ＋）
    /// 4．友人宅へ本物の映像を解析させる → インベントリに証拠の写真＋
    /// 5. 実行犯へ接触
    /// 6．ラストシーンへ
    /// 

    string DataPath = Application.dataPath + "/StoryProgressSavedData.json";
    GameInfo _info;

    public void SaveData()
    {
        FramedInGameEventProgressData template = new FramedInGameEventProgressData();
        var progress = GameObject.FindObjectOfType<FlagManager>();
        template.Finished = progress.Progress;

        string jsonStr = JsonUtility.ToJson(template);

        StreamWriter sw = new StreamWriter(DataPath, false);
        sw.WriteLine(jsonStr);
        sw.Flush();
        sw.Close();
    }

    public FramedInGameEventProgressData ReadSaveData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/StoryProgressSavedData.json");
        string dataStr = sr.ReadToEnd();
        sr.Close();
        return JsonUtility.FromJson<FramedInGameEventProgressData>(dataStr);
    }

    /// <summary> データの取得ができればイニシャライズ </summary>
    /// <returns></returns>
    public void TryGetSetProgressData()
    {
        FramedInGameEventProgressData data = new();
        try
        {
            data = ReadSaveData();
        }
        catch (FileNotFoundException)   // もしなかった場合
        {
            SaveData(); // 生成
            data = ReadSaveData();
            var fMan = GameObject.FindObjectOfType<FlagManager>();
            fMan.OverwriteProgress(data.Finished);
        }
    }

    #region Story
    void Story(List<bool> progress)
    {

    }
    #endregion

    protected override void ToDoAtAwakeSingleton()
    {

    }
}

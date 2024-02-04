using SLib.Singleton;
using SLib.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField, Header("証拠品")]
    List<GameObject> Proofs;
    [SerializeField, Header("最終決戦シーン名")]
    string SceneName;

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
        StreamReader sr;
        string dataStr = "";
        sr = new StreamReader(Application.dataPath + "/StoryProgressSavedData.json");
        dataStr = sr.ReadToEnd();
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
    public void RunStory(List<bool> progress)   //InGameでのみ
    {
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();
        if (!progress[0])
        {
            Debug.Log("STORY 0");
            // 目的地を監視カメラのところへ
            var go = GameObject.Instantiate(Proofs[0]);
            var tr = GameObject.FindGameObjectWithTag("ProofCAM_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (!progress[1])
        {
            Debug.Log("STORY 1");
            // 目的地を凶器のとこへ
            var go = GameObject.Instantiate(Proofs[1]);
            var tr = GameObject.FindGameObjectWithTag("ProofKnife_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (!progress[2])
        {
            Debug.Log("STORY 2");
            // 写真を友人宅へ
            var go = GameObject.Instantiate(Proofs[2]);
            var tr = GameObject.FindGameObjectWithTag("ProofPic_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (progress[2])
        {
            // 最終決戦
            Debug.Log("To FINAL!");
            var sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.LoadSceneByName(SceneName);
        }
    }
    #endregion

    protected override void ToDoAtAwakeSingleton()
    {
    }
}

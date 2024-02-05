using SLib.Singleton;
using SLib.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    #region ProoEvents
    // ↓ カメラを集めたらナビからこれの呼び出し
    void InvokeEventOnCatchCamera()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_01");
        var p2 = GameObject.Find("Tomo_Dialogue_02");
        p1.SetActive(false);
        p2.SetActive(true);
        p1.GetComponent<SimpleConversation>().enabled = false;
        p2.GetComponent<SimpleConversation>().enabled = true;
        // ↓ ともにカメラを解析させるてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // とも宅へ ↓ 
        // ↓ Dialogue 02 で ストーリー進行 凶器回収へ
    }

    // ↓ 凶器を集めに行く

    // ↓ 凶器を集めたら友ナビからこれの呼び出し
    void InvokeEventOnCatchKnife()
    {
        var p = GameObject.Find("Tomo_Dialogue_01");
        var p1 = GameObject.Find("Tomo_Dialogue_02");
        var p2 = GameObject.Find("Tomo_Dialogue_03");
        p.SetActive(false);
        p1.SetActive(false);
        p2.SetActive(true);
        p1.GetComponent<SimpleConversation>().enabled = false;
        p2.GetComponent<SimpleConversation>().enabled = true;
        // ↓ ともの解析が終わったてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // 友ナビ ポップ → ともから「解析が終わった。面白いなこれ」
        var tnMan = GameObject.FindFirstObjectByType<TomoNaviManager>();
        tnMan.PopNavi(4);
        // とも宅へ ↓ 
        // ↓ Dialogue 03 で ストーリー 進行
    }

    // ↓ 写真を回収して真実を知る

    // ↓ 写真回収時の友ナビからこれを呼び出す
    void InvokeEventOnCatchPicture()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_03");
        var p2 = GameObject.Find("Tomo_Dialogue_04");
        p1.SetActive(false);
        p2.SetActive(true);
        p1.GetComponent<SimpleConversation>().enabled = false;
        p2.GetComponent<SimpleConversation>().enabled = true;
        // ↓ ともの解析が終わったてい
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // 友ナビ ポップ → ともから「解析が終わった。面白いなこれ」
        // とも宅へ ↓ 
        // ↓ Dialogue 04 で ストーリー 進行
    }
    #endregion

    #region Story
    public void RunStory(List<bool> progress)   //InGameでのみ
    {
        Debug.Log("Story Core Running");
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();

        if (!progress[0])
        {
            /// 友ナビで「ニュースになってるぞ！話を聞かせろ！」
            /// 友人宅へ ポスターの機能を流用してやる →「お前がやっていないのなら証拠が必要だ。」
            /// 事件現場へ行って証拠になりそうなものをとりに行け！
            /// ↑ 友ナビからの呼び出しでここでしょりしなくてOK

            // 目的地を監視カメラのところへ
            var go = GameObject.Instantiate(Proofs[0]);
            var tr = GameObject.FindGameObjectWithTag("ProofCAM_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);

            /// 回収されたなら友ナビをポップそれベースでイベント呼び出しをする
        }
        else if (!progress[1])
        {
            //InvokeEventOnCatchCamera();
            /// 友人宅にて 「これの映像が暗号化されてる、解析に時間がかかりそうだ。そのあいだにもっと決定的なものを探してくれ」
            /// 

            // 目的地を凶器のとこへ
            var go = GameObject.Instantiate(Proofs[1]);
            var tr = GameObject.FindGameObjectWithTag("ProofKnife_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (!progress[2])
        {
            //InvokeEventOnCatchKnife();
            /// 友ナビ 「解析結果の画像を印刷した取りに来い！」
            /// 

            // 写真を友人宅へ
            var go = GameObject.Instantiate(Proofs[2]);
            var tr = GameObject.FindGameObjectWithTag("ProofPic_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (progress[2])
        {
            //InvokeEventOnCatchPicture();
            // 最終決戦
            var sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.LoadSceneByName(SceneName);
        }
    }
    #endregion

    protected override void ToDoAtAwakeSingleton()
    {
    }
}

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
    /// #�C���x���g���C���f�b�N�X#
    /// 0 - �Ď��J����
    /// 1 - ����̃i�C�t
    /// 2 - �؋��̝s���O�̎ʐ^
    /// 

    public List<bool> Finished;
}

public class FramedEventsInGameGeneralManager : SingletonBaseClass<FramedEventsInGameGeneralManager>
{
    /// ���t���[��
    /// 2�D�Ď��J���������� �� �F�l��ցi�C���x���g���ǉ��j
    /// 3�D���s���ċ����T���Ɂi�C���x���g���{�j
    /// 4�D�F�l��֖{���̉f������͂����� �� �C���x���g���ɏ؋��̎ʐ^�{
    /// 5. ���s�Ƃ֐ڐG
    /// 6�D���X�g�V�[����
    /// 

    [SerializeField, Header("�؋��i")]
    List<GameObject> Proofs;
    [SerializeField, Header("�ŏI����V�[����")]
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

    /// <summary> �f�[�^�̎擾���ł���΃C�j�V�����C�Y </summary>
    /// <returns></returns>
    public void TryGetSetProgressData()
    {
        FramedInGameEventProgressData data = new();
        try
        {
            data = ReadSaveData();
        }
        catch (FileNotFoundException)   // �����Ȃ������ꍇ
        {
            SaveData(); // ����
            data = ReadSaveData();
            var fMan = GameObject.FindObjectOfType<FlagManager>();
            fMan.OverwriteProgress(data.Finished);
        }
    }

    #region Story
    public void RunStory(List<bool> progress)   //InGame�ł̂�
    {
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();
        if (!progress[0])
        {
            Debug.Log("STORY 0");
            // �ړI�n���Ď��J�����̂Ƃ����
            var go = GameObject.Instantiate(Proofs[0]);
            var tr = GameObject.FindGameObjectWithTag("ProofCAM_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (!progress[1])
        {
            Debug.Log("STORY 1");
            // �ړI�n������̂Ƃ���
            var go = GameObject.Instantiate(Proofs[1]);
            var tr = GameObject.FindGameObjectWithTag("ProofKnife_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (!progress[2])
        {
            Debug.Log("STORY 2");
            // �ʐ^��F�l���
            var go = GameObject.Instantiate(Proofs[2]);
            var tr = GameObject.FindGameObjectWithTag("ProofPic_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (progress[2])
        {
            // �ŏI����
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

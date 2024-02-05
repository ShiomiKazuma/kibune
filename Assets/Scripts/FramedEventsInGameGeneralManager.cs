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

    #region ProoEvents
    // �� �J�������W�߂���i�r���炱��̌Ăяo��
    void InvokeEventOnCatchCamera()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_01");
        var p2 = GameObject.Find("Tomo_Dialogue_02");
        p1.SetActive(false);
        p2.SetActive(true);
        p1.GetComponent<SimpleConversation>().enabled = false;
        p2.GetComponent<SimpleConversation>().enabled = true;
        // �� �Ƃ��ɃJ��������͂�����Ă�
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // �Ƃ���� �� 
        // �� Dialogue 02 �� �X�g�[���[�i�s ��������
    }

    // �� ������W�߂ɍs��

    // �� ������W�߂���F�i�r���炱��̌Ăяo��
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
        // �� �Ƃ��̉�͂��I������Ă�
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // �F�i�r �|�b�v �� �Ƃ�����u��͂��I������B�ʔ����Ȃ���v
        var tnMan = GameObject.FindFirstObjectByType<TomoNaviManager>();
        tnMan.PopNavi(4);
        // �Ƃ���� �� 
        // �� Dialogue 03 �� �X�g�[���[ �i�s
    }

    // �� �ʐ^��������Đ^����m��

    // �� �ʐ^������̗F�i�r���炱����Ăяo��
    void InvokeEventOnCatchPicture()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_03");
        var p2 = GameObject.Find("Tomo_Dialogue_04");
        p1.SetActive(false);
        p2.SetActive(true);
        p1.GetComponent<SimpleConversation>().enabled = false;
        p2.GetComponent<SimpleConversation>().enabled = true;
        // �� �Ƃ��̉�͂��I������Ă�
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // �F�i�r �|�b�v �� �Ƃ�����u��͂��I������B�ʔ����Ȃ���v
        // �Ƃ���� �� 
        // �� Dialogue 04 �� �X�g�[���[ �i�s
    }
    #endregion

    #region Story
    public void RunStory(List<bool> progress)   //InGame�ł̂�
    {
        Debug.Log("Story Core Running");
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();

        if (!progress[0])
        {
            /// �F�i�r�Łu�j���[�X�ɂȂ��Ă邼�I�b�𕷂�����I�v
            /// �F�l��� �|�X�^�[�̋@�\�𗬗p���Ă�� ���u���O������Ă��Ȃ��̂Ȃ�؋����K�v���B�v
            /// ��������֍s���ď؋��ɂȂ肻���Ȃ��̂��Ƃ�ɍs���I
            /// �� �F�i�r����̌Ăяo���ł����ł���肵�Ȃ���OK

            // �ړI�n���Ď��J�����̂Ƃ����
            var go = GameObject.Instantiate(Proofs[0]);
            var tr = GameObject.FindGameObjectWithTag("ProofCAM_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);

            /// ������ꂽ�Ȃ�F�i�r���|�b�v����x�[�X�ŃC�x���g�Ăяo��������
        }
        else if (!progress[1])
        {
            //InvokeEventOnCatchCamera();
            /// �F�l��ɂ� �u����̉f�����Í�������Ă�A��͂Ɏ��Ԃ������肻�����B���̂������ɂ����ƌ���I�Ȃ��̂�T���Ă���v
            /// 

            // �ړI�n������̂Ƃ���
            var go = GameObject.Instantiate(Proofs[1]);
            var tr = GameObject.FindGameObjectWithTag("ProofKnife_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (!progress[2])
        {
            //InvokeEventOnCatchKnife();
            /// �F�i�r �u��͌��ʂ̉摜������������ɗ����I�v
            /// 

            // �ʐ^��F�l���
            var go = GameObject.Instantiate(Proofs[2]);
            var tr = GameObject.FindGameObjectWithTag("ProofPic_Pos").transform;
            go.transform.position = tr.position;
            go.transform.rotation = tr.rotation;
            mapI.SetTarget(go.transform);
        }
        else if (progress[2])
        {
            //InvokeEventOnCatchPicture();
            // �ŏI����
            var sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
            sceneLoader.LoadSceneByName(SceneName);
        }
    }
    #endregion

    protected override void ToDoAtAwakeSingleton()
    {
    }
}

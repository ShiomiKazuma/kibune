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
    void Story(List<bool> progress)
    {

    }
    #endregion

    protected override void ToDoAtAwakeSingleton()
    {

    }
}

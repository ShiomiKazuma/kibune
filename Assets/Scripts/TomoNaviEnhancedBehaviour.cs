using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �F�i�r�̋@�\�g���N���X
public class TomoNaviEnhancedBehaviour : MonoBehaviour
{
    // ����
    public void SetObjectiveToSafeHouse()
    {
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectsWithTag("SafeHouse_Pos")[0].transform; // SafeHouse_Pos
        Debug.Log($"TomoNaviEnhancedBehaviour 0 {dest.position.ToString()}");
        mapObj.SetTarget(dest);
        // �܂��Ƃ���� �� 
        // �� Dialogue 01 �X�g�[���[�i�s �J���������
        var storyStarter = GameObject.FindFirstObjectByType<StoryStarter>();
        storyStarter.RunStory();
    }

    // �� �J�������W�߂ɍs��

    // �� �J�������W�߂���i�r���炱��̌Ăяo��
    public void InvokeEventOnCatchCamera()
    {
        var p1 = GameObject.Find("Tomo_Dialogue_01");
        var p2 = GameObject.Find("Tomo_Dialogue_02");
        p1.GetComponent<SimpleConversation>().enabled = (false);
        p2.GetComponent<SimpleConversation>().enabled = (true);
        // �� �Ƃ��ɃJ��������͂�����Ă�
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // �Ƃ���� �� 
        // �� Dialogue 02 �� �X�g�[���[�i�s ��������
    }

    // �� ������W�߂ɍs��

    // �� ������W�߂���F�i�r���炱��̌Ăяo��
    public void InvokeEventOnCatchKnife()
    {
        var p = GameObject.Find("Tomo_Dialogue_01");
        var p1 = GameObject.Find("Tomo_Dialogue_02");
        var p2 = GameObject.Find("Tomo_Dialogue_03");
        p.GetComponent<SimpleConversation>().enabled = (false);
        p1.GetComponent<SimpleConversation>().enabled = (false);
        p2.GetComponent<SimpleConversation>().enabled = (true);
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
    public void InvokeEventOnCatchPicture()
    {
        //var p = GameObject.Find("Tomo_Dialogue_01");
        //var p0 = GameObject.Find("Tomo_Dialogue_02");
        //var p1 = GameObject.Find("Tomo_Dialogue_03");
        //p.GetComponent<SimpleConversation>().enabled = (false);
        //p0.GetComponent<SimpleConversation>().enabled = (false);
        //p1.GetComponent<SimpleConversation>().enabled = (true);
        //// �� �Ƃ��̉�͂��I������Ă�
        //var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        //var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        //mapObj.SetTarget(dest);
        // �F�i�r �|�b�v �� �Ƃ�����u��͂��I������B�ʔ����Ȃ���v
        // �Ƃ���� �� 
        // �� Dialogue 04 �� �X�g�[���[ �i�s

        var p = GameObject.Find("Tomo_Dialogue_01");
        var p0 = GameObject.Find("Tomo_Dialogue_02");
        var p1 = GameObject.Find("Tomo_Dialogue_03");
        var p2 = GameObject.Find("Tomo_Dialogue_04");
        p.GetComponent<SimpleConversation>().enabled = (false);
        p0.GetComponent<SimpleConversation>().enabled = (false);
        p1.GetComponent<SimpleConversation>().enabled = (true);
        p2.GetComponent<SimpleConversation>().enabled = (false);
        // �� �Ƃ��̉�͂��I������Ă�
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // �F�i�r �|�b�v �� �Ƃ�����u��͂��I������B�ʔ����Ȃ���v
        // �Ƃ���� �� 
        // �� Dialogue 04 �� �X�g�[���[ �i�s
    }

    public void InvokeEventToStagePicture()
    {
        var hoge = GameObject.FindFirstObjectByType<FramedEventsInGameGeneralManager>();
        hoge.SaveData();
        hoge.TryGetSetProgressData();
        var data = hoge.ReadSaveData();
        hoge.RunStory(data.Finished);
        // �o�O�����炱���� ctrl + z
    }

    public void InvokeEventToLastChase()
    {
        var p = GameObject.Find("Tomo_Dialogue_01");
        var p0 = GameObject.Find("Tomo_Dialogue_02");
        var p1 = GameObject.Find("Tomo_Dialogue_03");
        var p2 = GameObject.Find("Tomo_Dialogue_04");
        p.GetComponent<SimpleConversation>().enabled = (false);
        p0.GetComponent<SimpleConversation>().enabled = (false);
        p1.GetComponent<SimpleConversation>().enabled = (false);
        p2.GetComponent<SimpleConversation>().enabled = (true);
        // �� �Ƃ��̉�͂��I������Ă�
        var mapObj = GameObject.FindFirstObjectByType<ObjectiveMapIndicator>();
        var dest = GameObject.FindGameObjectWithTag("SafeHouse_Pos").transform;
        mapObj.SetTarget(dest);
        // �F�i�r �|�b�v �� �Ƃ�����u��͂��I������B�ʔ����Ȃ���v
        // �Ƃ���� �� 
        // �� Dialogue 04 �� �X�g�[���[ �i�s
    }
}

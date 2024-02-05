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
        var dest = GameObject.Find("Tomo_Dialogue_01").transform;
        mapObj.SetTarget(dest);
        // �܂��Ƃ���� �� 
        // �� Dialogue 01 �X�g�[���[�i�s �J���������
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
        var p1 = GameObject.Find("Tomo_Dialogue_02");
        var p2 = GameObject.Find("Tomo_Dialogue_03");
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
        var p1 = GameObject.Find("Tomo_Dialogue_03");
        var p2 = GameObject.Find("Tomo_Dialogue_04");
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

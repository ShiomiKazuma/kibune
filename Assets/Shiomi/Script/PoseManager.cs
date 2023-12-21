using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    /// <summary> �|�[�Y��ʂɓ������Ƃ��ɌĂ΂�郁�\�b�h </summary>
    public static Action BeginPause;
    /// <summary> �|�[�Y��ʂ��I��������ɌĂ΂�郁�\�b�h </summary>
    public static Action EndPause;
    /// <summary> �|�[�Y��Ԃł��邩�̃t���O </summary>
    bool IsPause;

    private void OnEnable()
    {
        //�f���Q�[�g�o�^
        BeginPause += StartPause;
        EndPause += PauseEnd;
    }

    private void OnDisable()
    {
        //�f���Q�[�g����
        BeginPause -= StartPause;
        EndPause -= PauseEnd;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�|�[�Y�{�^������������A����������
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(IsPause)
            {
                EndPause();
            }
            else
            {
                BeginPause();
            }
        }
    }

    void StartPause()
    {

    }

    void PauseEnd()
    {

    }
}

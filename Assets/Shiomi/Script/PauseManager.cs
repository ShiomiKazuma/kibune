using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour   // �@�Q�[���݂̂̃C���X�^���X�V���O���g���ł���K�v�Ȃ�
{
    /// <summary> �|�[�Y��ʂɓ������Ƃ��ɌĂ΂�郁�\�b�h </summary>
    public static Action BeginPause;
    /// <summary> �|�[�Y��ʂ��I��������ɌĂ΂�郁�\�b�h </summary>
    public static Action EndPause;
    /// <summary> �|�[�Y��Ԃł��邩�̃t���O </summary>
    bool IsPause;
    [SerializeField] GameObject _inventoryUI;

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
        if (Input.GetButtonDown("Inventory"))
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

        // �i�����j �ݒ��ʗp�̓��͂����������̏���������ȍ~�ɂ͂��ޗ\��
    }

    void StartPause()
    {
         _inventoryUI.SetActive(!_inventoryUI.activeSelf);
    }

    void PauseEnd()
    {
        _inventoryUI.SetActive(!_inventoryUI.activeSelf);
    }
}

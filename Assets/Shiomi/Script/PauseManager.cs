using SLib.Systems;
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
    bool _isPaused;
    public bool IsPaused => _isPaused;

    [SerializeField] GameObject _inventoryUI;
    HUDManager _hudMan;

    private void Awake() => _hudMan = GameObject.FindObjectOfType<HUDManager>();

    // Update is called once per frame
    void Update()
    {
        //�|�[�Y�{�^������������A����������
        if (Input.GetButtonDown("Inventory"))
        {
            _isPaused = !_isPaused;

            if (IsPaused)
            {
                EndPause();
                _inventoryUI.SetActive(!_isPaused);
                _inventoryUI.transform.SetAsFirstSibling();
            }
            else
            {
                BeginPause();
                _inventoryUI.SetActive(_isPaused);
                _inventoryUI.transform.SetAsLastSibling();
            }
        }

        // �i�����j �ݒ��ʗp�̓��͂����������̏���������ȍ~�ɂ͂��ޗ\��
        if (Input.GetKeyDown("Settings"))
        {
            _isPaused = !_isPaused;

            if (IsPaused)
            {
                EndPause();
                _hudMan.ToFront(2);
            }
            else
            {
                BeginPause();
                _hudMan.ToFront(1);
            }
        }
    }
}

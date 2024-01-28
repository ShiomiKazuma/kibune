using SLib.Singleton;
using SLib.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : SingletonBaseClass<PauseManager>   // �@�Q�[���݂̂̃C���X�^���X�V���O���g���ł���K�v�Ȃ�
{
    [SerializeField] GameObject _inventoryUI;
    /// <summary> �|�[�Y��ʂɓ������Ƃ��ɌĂ΂�郁�\�b�h </summary>
    public static Action BeginPause;
    /// <summary> �|�[�Y��ʂ��I��������ɌĂ΂�郁�\�b�h </summary>
    public static Action EndPause;

    HUDManager _hudMan;
    GameInfo _gInfo;

    /// <summary> �|�[�Y��Ԃł��邩�̃t���O </summary>
    bool _isPaused;
    public bool IsPaused => _isPaused;


    protected override void ToDoAtAwakeSingleton()
    {
        BeginPause += () => { };
        EndPause += () => { };
        _hudMan = GameObject.FindObjectOfType<HUDManager>();
        _gInfo = GameObject.FindObjectOfType<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only InGame Scene This Will Be Processed
        if (_gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_TitleScene
            || _gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_UniqueScene)
            return;

        //�|�[�Y�{�^������������A����������
        if (Input.GetButtonDown("Inventory"))
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
                _hudMan.ToFront(3);
            }
        }

        // �i�����j �ݒ��ʗp�̓��͂����������̏���������ȍ~�ɂ͂��ޗ\��
        if (Input.GetButtonDown("Settings"))
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

using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
// �쐬 �����ʂ�
namespace SLib
{
    namespace Systems
    {
        enum AppQuitStatus
        {
            Title,
            InGame,
        }

        public class ApplicationQuitter : SingletonBaseClass<ApplicationQuitter>
        {
            [SerializeField, Header("�^�C�g����ʂȂ炱���ɉ����A�^�b�`���Ȃ��Ă�OK")]
            Transform _playerTransform;
            [SerializeField, Header("�^�C�g�����C���Q�[�����̑I��������")]
            AppQuitStatus _appStatus;

            PlayerSaveDataCreator _playerSaveDataCreator;

            protected override void ToDoAtAwakeSingleton()
            {
                _playerSaveDataCreator = GameObject.FindFirstObjectByType<PlayerSaveDataCreator>();
            }

            public void QuitApplication()
            {
                #region PreProcess
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
                #endregion

                switch (_appStatus)
                {
                    case AppQuitStatus.InGame:
                        _playerSaveDataCreator.SavePlayerData(_playerTransform, SceneManager.GetActiveScene().name);
                        break;
                    case AppQuitStatus.Title: break;
                }
                Application.Quit();
            }
        }

    }
}

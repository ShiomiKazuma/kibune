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
            GameInfo _gameInfo;

            protected override void ToDoAtAwakeSingleton()
            {
                SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
                _playerSaveDataCreator = GameObject.FindFirstObjectByType<PlayerSaveDataCreator>();
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
            }
            void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
            {
                if (arg1.name != _gameInfo.TitleSceneName || arg0.name == _gameInfo.TitleSceneName)
                {
                    _appStatus = AppQuitStatus.InGame;
                }
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

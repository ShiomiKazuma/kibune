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
        public class ApplicationQuitter : SingletonBaseClass<ApplicationQuitter>
        {
            [SerializeField, Header("�^�C�g����ʂȂ炱���ɉ����A�^�b�`���Ȃ��Ă�OK")]
            Transform _playerTransform;

            PlayerSaveDataCreator _playerSaveDataCreator;
            GameInfo _gameInfo;

            protected override void ToDoAtAwakeSingleton()
            {
                _playerSaveDataCreator = GameObject.FindFirstObjectByType<PlayerSaveDataCreator>();
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
            }
            public void QuitApplication()
            {
                #region PreProcess
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
                #endregion

                switch (_gameInfo.SceneStatus)
                {
                    case GameInfo.SceneTransitStatus.To_InGameScene:
                        if (_playerTransform == null)
                        {
                            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                        }
                        _playerSaveDataCreator.SavePlayerData(_playerTransform, SceneManager.GetActiveScene().name);
                        break;
                    case GameInfo.SceneTransitStatus.To_TitleScene: break;
                    case GameInfo.SceneTransitStatus.To_UniqueScene: break;
                }
                Application.Quit();
            }
        }

    }
}

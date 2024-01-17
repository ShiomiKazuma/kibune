using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
// 作成 すがぬま
namespace SLib
{
    namespace Systems
    {
        public class ApplicationQuitter : SingletonBaseClass<ApplicationQuitter>
        {
            [SerializeField, Header("タイトル画面ならここに何もアタッチしなくてもOK")]
            Transform _playerTransform;
            [SerializeField, Header("タイトルかインゲームかの選択をする")]
            GameInfo.SceneTransitStatus _appStatus;

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
                if (arg1.name == "Prolougue" || arg1.name == "Epilougue")
                {
                    _appStatus = GameInfo.SceneTransitStatus.To_UniqueScene;
                }
                else if (arg1.name != _gameInfo.TitleSceneName || arg0.name == _gameInfo.TitleSceneName)
                {
                    _appStatus = GameInfo.SceneTransitStatus.To_InGameScene;
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
                    case GameInfo.SceneTransitStatus.To_InGameScene:
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

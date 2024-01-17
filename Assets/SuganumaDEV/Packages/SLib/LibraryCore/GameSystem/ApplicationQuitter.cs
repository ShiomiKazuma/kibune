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
        enum AppQuitStatus
        {
            Title,
            InGame,
        }

        public class ApplicationQuitter : SingletonBaseClass<ApplicationQuitter>
        {
            [SerializeField, Header("タイトル画面ならここに何もアタッチしなくてもOK")]
            Transform _playerTransform;
            [SerializeField, Header("タイトルかインゲームかの選択をする")]
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

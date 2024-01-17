using SLib.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//作成 菅沼
namespace SLib
{
    namespace Systems
    {
        [Serializable]
        public class SaveDataTemplate
        {
            public Vector3 _lastStandingPosition;       // Pos
            public Quaternion _lastStandingRotation;       // Rot
            public string _sceneName;                      // Scene Name
        }

        enum SceneStatus
        {
            Title,
            InGame,
        }

        /// <summary> 渡されたフィールドの値をもとにScriptableObjectを生成、DataPath直下へ格納。 </summary>
        public class PlayerSaveDataCreator : SingletonBaseClass<PlayerSaveDataCreator>  // セーブデータの保存
        {
            [SerializeField, Header("タイトル画面ならここに何もアタッチしなくてもOK")]
            Transform _playerTransform;
            [SerializeField, Header("タイトルかインゲームかの選択をする")]
            SceneStatus _sceneStatus;

            GameInfo _gameInfo;
            protected override void ToDoAtAwakeSingleton()
            {
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
                SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            }

            private void Start()
            {
                SavePlayerDataAutomatically();
            }

            void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
            {
                if (arg1.name != _gameInfo.TitleSceneName || arg0.name == _gameInfo.TitleSceneName)
                {
                    _sceneStatus = SceneStatus.InGame;
                }
            }

            string _playerDataPath = Application.dataPath + "/PlayerSavedData.json";

            public void SavePlayerDataAutomatically()
            {
                switch (_sceneStatus)
                {
                    case SceneStatus.Title:
                        break;
                    case SceneStatus.InGame:
                        SavePlayerData(_playerTransform, SceneManager.GetActiveScene().name);
                        break;
                }
            }

            public void SavePlayerData(Transform playerStandingTransform, string sceneName)
            {
                SaveDataTemplate template = new SaveDataTemplate();
                template._lastStandingPosition = playerStandingTransform.position;
                template._lastStandingRotation = playerStandingTransform.rotation;
                template._sceneName = sceneName;

                string jsonStr = JsonUtility.ToJson(template);

                StreamWriter sw = new StreamWriter(_playerDataPath, false);
                sw.WriteLine(jsonStr);
                sw.Flush();
                sw.Close();
            }
        }
    }
}

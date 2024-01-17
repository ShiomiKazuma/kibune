using SLib.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//�쐬 ����
namespace SLib
{
    namespace Systems
    {
        [Serializable]
        public class SaveDataTemplate
        {
            public Vector3 _lastStandingPosition;           // Pos
            public Quaternion _lastStandingRotation;       // Rot
            public string _sceneName;                      // Scene Name
        }

        enum SceneStatus
        {
            Title,
            UniqueScene,
            InGame,
        }

        /// <summary> �n���ꂽ�t�B�[���h�̒l�����Ƃ�ScriptableObject�𐶐��ADataPath�����֊i�[�B </summary>
        public class PlayerSaveDataCreator : SingletonBaseClass<PlayerSaveDataCreator>  // �Z�[�u�f�[�^�̕ۑ�
        {
            [SerializeField, Header("�^�C�g����ʂȂ炱���ɉ����A�^�b�`���Ȃ��Ă�OK")]
            Transform _playerTransform;
            [SerializeField, Header("�^�C�g�����C���Q�[�����̑I��������")]
            GameInfo.SceneTransitStatus _sceneStatus;

            GameInfo _gameInfo;
            protected override void ToDoAtAwakeSingleton()
            {
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
                SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            }

            void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
            {
                if (arg1.name == "Prolougue" || arg1.name == "Epilougue")
                {
                    _sceneStatus = GameInfo.SceneTransitStatus.To_UniqueScene;
                }
                else if (arg1.name != _gameInfo.TitleSceneName || arg0.name == _gameInfo.TitleSceneName)
                {
                    _sceneStatus = GameInfo.SceneTransitStatus.To_InGameScene;
                    SavePlayerDataAutomatically();
                }
            }

            string _playerDataPath = Application.dataPath + "/PlayerSavedData.json";

            public void SavePlayerDataAutomatically()
            {
                switch (_sceneStatus)
                {
                    case GameInfo.SceneTransitStatus.To_TitleScene:
                        break;
                    case GameInfo.SceneTransitStatus.To_InGameScene:
                        if (_playerTransform == null)
                        {
                            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                        }
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

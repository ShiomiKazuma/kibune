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
            public Vector3 _lastStandingPosition;       // Pos
            public Quaternion _lastStandingRotation;       // Rot
            public string _sceneName;                      // Scene Name
        }

        enum SceneStatus
        {
            Title,
            InGame,
        }

        /// <summary> �n���ꂽ�t�B�[���h�̒l�����Ƃ�ScriptableObject�𐶐��ADataPath�����֊i�[�B </summary>
        public class PlayerSaveDataCreator : SingletonBaseClass<PlayerSaveDataCreator>  // �Z�[�u�f�[�^�̕ۑ�
        {
            [SerializeField]
            Transform _playerTransform;
            [SerializeField]
            SceneStatus _sceneStatus;
            protected override void ToDoAtAwakeSingleton()
            {
                SavePlayerDataAutomatically();
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

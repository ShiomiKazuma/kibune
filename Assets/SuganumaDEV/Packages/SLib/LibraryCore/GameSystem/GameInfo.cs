using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// auth ����
namespace SLib
{
    namespace Systems
    {
        public class GameInfo : SingletonBaseClass<GameInfo>
        {
            /// <summary> �J�ڐ�V�[�����ǂ̂悤�Ȃ��̂���I���A�ێ����� </summary>
            public enum SceneTransitStatus
            {
                To_TitleScene,
                To_UniqueScene,
                To_InGameScene,
            }

            [SerializeField]
            string _titleSceneName;
            [SerializeField]
            SceneTransitStatus _sceneStat;
            // �O������`�����v���p�e�B
            public SceneTransitStatus SceneStatus { get { return _sceneStat; } set { _sceneStat = value; } }

            public string TitleSceneName { get { return _titleSceneName; } }

            protected override void ToDoAtAwakeSingleton()
            {
                SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

                void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
                {
                    if (arg1.name == "Prolougue" || arg1.name == "Epilougue" || arg1.name == "StaffRoll")
                    {
                        _sceneStat = GameInfo.SceneTransitStatus.To_UniqueScene;
                    }
                    else if (arg1.name != TitleSceneName || arg0.name == TitleSceneName)
                    {
                        _sceneStat = GameInfo.SceneTransitStatus.To_InGameScene;
                    }
                }
            }
        }
    }
}

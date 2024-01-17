using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SLib
{
    namespace Systems
    {
        public class GameInfo : SingletonBaseClass<GameInfo>
        {
            [SerializeField]
            string _titleSceneName;

            /// <summary> �J�ڐ�V�[�����ǂ̂悤�Ȃ��̂���I���A�ێ����� </summary>
            public enum SceneTransitStatus
            {
                To_TitleScene,
                To_UniqueScene,
                To_InGameScene,
            }

            public string TitleSceneName { get { return _titleSceneName; } }

            protected override void ToDoAtAwakeSingleton() { }
        }
    }
}

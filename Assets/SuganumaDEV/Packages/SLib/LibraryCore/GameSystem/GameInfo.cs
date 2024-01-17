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

            /// <summary> 遷移先シーンがどのようなものかを選択、保持する </summary>
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

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

            public string TitleSceneName { get { return _titleSceneName; } }

            protected override void ToDoAtAwakeSingleton() { }
        }
    }
}

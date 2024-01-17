using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// auth suganuma
namespace SLib
{
    namespace Systems
    {
        /// <summary> list 登録されたHUDを管理する </summary>
        public class HUDManager : SingletonBaseClass<HUDManager>     // list 最後尾が一番後ろ
        {
            [SerializeField]
            List<GameObject> _huds;

            public void ToFront(int index)
            {
                _huds[index].transform.SetAsLastSibling();
            }

            protected override void ToDoAtAwakeSingleton()
            {
            }
        }
    }
}

using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// auth suganuma
namespace SLib
{
    namespace Systems
    {
        /// <summary> list 登録されたHUDを管理する </summary>
        public class HUDManager : SingletonBaseClass<HUDManager>     // list 最後尾が一番後ろ
        {
            [SerializeField, Header("各HUDの親オブジェクト")]
            GameObject _allHUDParent;
            [SerializeField, Header("各HUDモジュール ＃モジュールを＋で追加すること。絶対に並び変えない＃")]
            List<GameObject> _huds;

            SceneLoader _sceneLoader;

            public void ToFront(int index)
            {
                var huds = _allHUDParent.GetChildObjects();
                for (int i = 0; i < _allHUDParent.transform.childCount; i++)
                {
                    huds[i].gameObject.SetActive(false);
                }
                _huds[index].transform.SetAsLastSibling();
                _huds[index].gameObject.SetActive(true);
            }

            protected override void ToDoAtAwakeSingleton()
            {
                GameObject.DontDestroyOnLoad(_allHUDParent);
                _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
            }
        }
    }
}

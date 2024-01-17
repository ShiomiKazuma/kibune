using SLib.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
// Auth : Suganuma
namespace SLib
{
    namespace Systems
    {
        public class SceneLoader : SingletonBaseClass<SceneLoader>
        {
            [SerializeField]
            GameObject _nowLoadingPanel;

            public void LoadSceneByName(string sceneName)
            {
                StartCoroutine(LoadSceneAcyncByName(sceneName));
            }

            protected override void ToDoAtAwakeSingleton()
            {
                _nowLoadingPanel.SetActive(false);
                _nowLoadingPanel.transform.SetAsFirstSibling();
            }

            IEnumerator LoadSceneAcyncByName(string sceneName)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
                while (!asyncLoad.isDone)
                {
                    _nowLoadingPanel.transform.SetAsLastSibling();
                    _nowLoadingPanel.SetActive(!false);
                    yield return null;
                }
            }
        }
    }
}

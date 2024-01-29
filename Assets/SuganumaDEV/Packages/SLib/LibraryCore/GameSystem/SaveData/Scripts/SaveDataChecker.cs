using SLib.Singleton;
using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Events;
// auth Suganuma
namespace SLib
{
    namespace Systems
    {
        public class SaveDataChecker : SingletonBaseClass<SaveDataChecker>
        {
            [SerializeField]
            Button _continueButton;
            [SerializeField]
            PlayerSaveDataSerializer _dataSerializer;

            GameInfo _gameInfo;
            SceneLoader _sceneLoader;

            protected override void ToDoAtAwakeSingleton()
            {
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
                _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();

                if (_gameInfo.TitleSceneName != SceneManager.GetActiveScene().name) return; // If Active Scene Is NOT TitleScene, DoNothing

                try
                {
                    if (_dataSerializer.ReadSaveData() != null) // If SavedData Couldn't found , Exception Will Threw
                    {
                        _continueButton.interactable = true;
                        _continueButton.gameObject.SetActive(true);
                    }
                }
                catch (FileNotFoundException)
                {
                    _continueButton.interactable = false;
                    _continueButton.gameObject.SetActive(false);
                }
            }
        }
    }
}

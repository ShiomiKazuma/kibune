using SLib.Singleton;
using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
// auth Suganuma
namespace SLib
{
    namespace Systems
    {
        public class SaveDataChecker : SingletonBaseClass<SaveDataChecker>
        {
            [SerializeField]
            Text _startGameButtonsText;
            [SerializeField]
            PlayerSaveDataSerializer _dataSerializer;

            GameInfo _gameInfo;
            protected override void ToDoAtAwakeSingleton()
            {
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
                if (_gameInfo.TitleSceneName != SceneManager.GetActiveScene().name) return; // If Active Scene Is NOT TitleScene, DoNothing

                try
                {
                    if (_dataSerializer.ReadSaveData() != null)
                    {
                        _startGameButtonsText.text = "Continue Saga";
                    }
                }
                catch (FileNotFoundException)
                {
                    _startGameButtonsText.text = "Start Saga";
                }
            }
        }
    }
}

using SLib.Singleton;
using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

                if (_dataSerializer.ReadSaveData() != null)
                {
                    _startGameButtonsText.text = "Continue Game";
                }
            }
        }
    }
}

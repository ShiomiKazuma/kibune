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
            Button _startBtn;
            [SerializeField]
            PlayerSaveDataSerializer _dataSerializer;
            [SerializeField]
            string _newGamedSceneName;
            [SerializeField]
            string _continuedSceneName;

            Text _startGameButtonsText;
            GameInfo _gameInfo;
            SceneLoader _sceneLoader;

            protected override void ToDoAtAwakeSingleton()
            {
                _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
                _startGameButtonsText = _startBtn.GetComponentInChildren<Text>();
                _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();

                if (_gameInfo.TitleSceneName != SceneManager.GetActiveScene().name) return; // If Active Scene Is NOT TitleScene, DoNothing

                _startBtn.onClick.RemoveAllListeners();
            }

            private void Start()
            {
                UnityAction _UANewGame = new UnityAction(() => { _sceneLoader.LoadSceneByName(_newGamedSceneName); });
                UnityAction _UAContinueGame = new UnityAction(() => { _sceneLoader.LoadSceneByName(_continuedSceneName); });
                try
                {
                    if (_dataSerializer.ReadSaveData() != null) // If SavedData Couldn't found , Exception Will Threw
                    {
                        _startGameButtonsText.text = "Continue";
                        _startBtn.onClick.AddListener(_UAContinueGame);
                    }
                }
                catch (FileNotFoundException)
                {
                    _startGameButtonsText.text = "New Game";
                    _startBtn?.onClick.AddListener(_UANewGame);
                }
            }
        }
    }
}

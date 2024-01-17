using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
// auth suganuma
public class OnSceneTransitEvents : SingletonBaseClass<OnSceneTransitEvents>, IOnSceneTransit
{
    GameInfo _gameInfo;
    SceneLoader _sceneLoader;
    GameObject _player;



    public void OnSceneTransitComplete(Scene scene)
    {
        if (scene.name == _gameInfo.TitleSceneName || scene.name == "Prolougue" || scene.name == "Epilougue")
        {

        }
        else if(scene.name == "InGameTesters")
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
        _sceneLoader._eventOnSceneLoaded.AddListener(OnSceneTransitComplete);
    }
}

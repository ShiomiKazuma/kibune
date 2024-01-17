using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
// auth suganuma
public class OnSceneTransitEvents : SingletonBaseClass<OnSceneTransitEvents>, IOnSceneTransit
{
    GameInfo _gameInfo;
    SceneLoader _sceneLoader;
    HUDManager _hudManager;

    public void OnSceneTransitComplete(Scene scene)
    {
        if (scene.name == _gameInfo.TitleSceneName || scene.name == "Prolougue" || scene.name == "Epilougue")
        {
            switch (scene.name)
            {
                case "Prolougue":
                    break;
                case "Epilougue":
                    break;
                default: break;
            }
        }
        else if (scene.name == "InGameTesters")
        {
            _hudManager.ToFront(2);
        }
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
        _sceneLoader._eventOnSceneLoaded.AddListener(OnSceneTransitComplete);
        _hudManager = GameObject.FindFirstObjectByType<HUDManager>();
    }
}

using SLib.Singleton;
using SLib.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// auth suganuma
public class OnSceneTransitEvents : SingletonBaseClass<OnSceneTransitEvents>, IOnSceneTransit
{
    GameInfo _gameInfo;
    SceneLoader _sceneLoader;

    public void OnSceneTransitComplete(Scene scene)
    {
        
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
        _sceneLoader._eventOnSceneLoaded.AddListener(OnSceneTransitComplete);
    }
}

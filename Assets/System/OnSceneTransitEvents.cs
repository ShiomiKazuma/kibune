using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// auth suganuma

public class OnSceneTransitEvents : SingletonBaseClass<OnSceneTransitEvents>, IOnSceneTransit
{
    GameInfo _gameInfo;
    SceneLoader _sceneLoader;
    HUDManager _hudManager;
    StaffRollAnimation _staffRollAnim;

    public void OnSceneTransitComplete(Scene scene)
    {
        _hudManager = GameObject.FindFirstObjectByType<HUDManager>();
        _staffRollAnim = GameObject.FindFirstObjectByType<StaffRollAnimation>();

        if (scene.name == _gameInfo.TitleSceneName || scene.name == "Prolougue" || scene.name == "Epilougue")
        {
            switch (scene.name)
            {
                case "Prolougue":
                    break;
                case "Epilougue":
                    break;
                default:// title scene
                    _hudManager.ToFront(0);
                    break;
            }
        }
        else if (scene.name == "InGameTesters")
        {
            _hudManager.ToFront(2);
        }
        else if (scene.name == "StaffRoll")
        {
            _hudManager.KillAll();
            _staffRollAnim.Invoke(nameof(_staffRollAnim.StartStaffRollAnimation), 1);
        }
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
        _sceneLoader._eventOnSceneLoaded.AddListener(OnSceneTransitComplete);
    }
}

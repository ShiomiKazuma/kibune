using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// auth suganuma

public class OnSceneTransitEvents : SingletonBaseClass<OnSceneTransitEvents>, IOnSceneTransit
{
    [SerializeField]
    GameObject staffRoll;
    [SerializeField]
    Button toTitle;
    [SerializeField]
    Button newGame;
    
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
                case "Epilougue":
                case "Prolougue":
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                default:// title scene
                    _hudManager.ToFront(0);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    staffRoll.gameObject.SetActive(true);
                    toTitle.gameObject.SetActive(false);
                    newGame.gameObject.SetActive(true);
                    break;
            }
        }
        else if (scene.name == "InGame")
        {
            _hudManager.ToFront(2);
            var eventProgMan = GameObject.FindAnyObjectByType<FramedEventsInGameGeneralManager>();
            eventProgMan.TryGetSetProgressData();

            var playerData = GameObject.FindAnyObjectByType<PlayerSaveDataSerializer>();
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

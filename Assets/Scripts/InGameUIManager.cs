using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : SingletonBaseClass<InGameUIManager>
{
    [SerializeField]
    Text _pausedText;
    [SerializeField]
    Button _settingsExit;

    PauseManager _pMan;
    GameInfo _gInfo;
    HUDManager _hudMan;

    void OnPaused()
    {
        _pausedText.text = "Paused...";
        _settingsExit.gameObject.SetActive(false);
    }

    void OnEndPaused()
    {
        _pausedText.text = " ";
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _pMan = GameObject.FindFirstObjectByType<PauseManager>();
    }

    private void Start()
    {
        _gInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _hudMan = GameObject.FindFirstObjectByType<HUDManager>();
        if (_gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_InGameScene)
        {
            _hudMan.ToFront(2);
        }
    }

    private void OnEnable()
    {
        _pMan.BeginPause += OnPaused;
        _pMan.EndPause += OnEndPaused;
    }

    private void OnDisable()
    {
        _pMan.BeginPause -= OnPaused;
        _pMan.EndPause -= OnEndPaused;
    }
}

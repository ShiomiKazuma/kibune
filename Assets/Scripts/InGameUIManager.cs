using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : SingletonBaseClass<InGameUIManager>
{
    [SerializeField]
    Text _pausedText;
    [SerializeField]
    Button _settingsExit;

    PauseManager _pMan;
    GameInfo _gInfo;

    void OnPaused()
    {
        _pausedText.text = "Paused...";
    }
    void OnEndPaused()
    {
        _pausedText.text = " ";
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _pMan = GameObject.FindFirstObjectByType<PauseManager>();
        _gInfo = GameObject.FindFirstObjectByType<GameInfo>();
        if (_gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_InGameScene)
        {
            _settingsExit.gameObject.SetActive(false);
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

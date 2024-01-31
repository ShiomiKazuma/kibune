using SLib.Singleton;
using SLib.Systems;
using UnityEngine;

public class GameManager : SingletonBaseClass<GameManager>
{
    GameInfo _gInfo;
    HUDManager _hudManager;

    public void GameOver()  // ˆê’â~‚µ‚ÄGOˆ—
    {
        PauseManager pm = GameObject.FindObjectOfType<PauseManager>();
        pm.CallBeginPause();
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _gInfo = GameObject.FindAnyObjectByType<GameInfo>();
        _hudManager = GameObject.FindAnyObjectByType<HUDManager>();
        if (_gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_InGameScene)
        {
            _hudManager.ToFront(2);
        }
    }
}

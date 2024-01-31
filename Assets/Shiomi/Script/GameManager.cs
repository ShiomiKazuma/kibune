using SLib.Singleton;
using SLib.Systems;
using UnityEngine;

public class GameManager : SingletonBaseClass<GameManager>
{
    GameInfo _gInfo;
    HUDManager _hudManager;

    public void GameOver()  // 一時停止してGO処理
    {
        PauseManager pm = GameObject.FindObjectOfType<PauseManager>();
        pm.BeginPause();
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

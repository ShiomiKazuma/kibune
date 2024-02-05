using SLib.Singleton;
using SLib.Systems;
using System;
using UnityEngine;

public class GameManager : SingletonBaseClass<GameManager>
{
    GameInfo _gInfo;
    HUDManager _hudManager;

    public event Action OnGameOver;

    public void GameOver()  // àÍéûí‚é~ÇµÇƒGOèàóù
    {
        PauseManager pm = GameObject.FindObjectOfType<PauseManager>();
        pm.CallBeginPause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (OnGameOver != null) OnGameOver();
        _hudManager = GameObject.FindAnyObjectByType<HUDManager>();
        _hudManager.ToFront(5);
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

using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : SingletonBaseClass<InGameUIManager>
{
    PauseManager _pMan;
    GameInfo _gInfo;
    HUDManager _hudMan;
    TomoNaviManager _tMan;

    protected override void ToDoAtAwakeSingleton()
    {
        _pMan = GameObject.FindFirstObjectByType<PauseManager>();
    }

    private void Start()
    {
        _gInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _hudMan = GameObject.FindFirstObjectByType<HUDManager>();
        _tMan = GameObject.FindFirstObjectByType<TomoNaviManager>();

        if (_gInfo.SceneStatus == GameInfo.SceneTransitStatus.To_InGameScene)
        {
            _hudMan.ToFront(2);
            Invoke(nameof(TomoNavi), 1);
        }
    }

    void TomoNavi()
    {
        _tMan.PopNavi(0);
    }
}

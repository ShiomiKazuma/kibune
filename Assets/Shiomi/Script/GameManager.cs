using SLib.Singleton;
using SLib.Systems;
using UnityEngine;

public class GameManager : SingletonBaseClass<GameManager>
{
    public static GameManager instance;
    [SerializeField, Header("�Q�[���I�[�o�[���")] string _gameOverSceneName;

    GameInfo _gInfo;
    HUDManager _hudManager;

    public void GameOver()
    {
        //�Q�[���I�[�o�[��ʂɑJ��
        UnityEngine.SceneManagement.SceneManager.LoadScene(_gameOverSceneName);
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

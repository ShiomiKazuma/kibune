using SLib.Singleton;
using SLib.Systems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
// auth suganuma
public class OnSceneTransitEvents : SingletonBaseClass<OnSceneTransitEvents>, IOnSceneTransit
{
    [SerializeField, Header("�t�F�[�h�A�E�g�̃V�O�i���A�Z�b�g")]
    SignalAsset _signalAsset;

    GameInfo _gameInfo;
    SceneLoader _sceneLoader;
    GameObject _player;

    void CallInGameScene()
    {
        _sceneLoader.LoadSceneByName("InGameTesters");
        throw new System.Exception("���ƂŖ{���̃C���Q�[���̃V�[���ɕς��邱��");
    }

    public void OnSceneTransitComplete(Scene scene)
    {
        if (scene.name == _gameInfo.TitleSceneName || scene.name == "Prolougue" || scene.name == "Epilougue")
        {
            switch (scene.name)
            {
                case "Prolougue":
                    break;
                case "Epilougue":
                    break;
                default: break;
            }
        }
        else if (scene.name == "InGameTesters")
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected override void ToDoAtAwakeSingleton()
    {
        _gameInfo = GameObject.FindFirstObjectByType<GameInfo>();
        _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
        _sceneLoader._eventOnSceneLoaded.AddListener(OnSceneTransitComplete);
    }
}

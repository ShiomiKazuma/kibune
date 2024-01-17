using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SLib.Systems; // <-
public class Fade : MonoBehaviour
{
    [SerializeField, Header("フェードさせるCanvas")] Image _canvasGroup;
    [SerializeField, Header("フェードする時間")] float _fadeTime;
    [SerializeField, Header("遷移させたいシーン名")] string _sceneName;

    SceneLoader _sceneLoader;   // <-

    #region AddedCodes
    private void Awake()
    {
        _sceneLoader = GameObject.FindFirstObjectByType<SceneLoader>();
    }
    #endregion  // <-

    public void FadeIn()
    {
        var color = _canvasGroup.color;
        color.a = 1;
        _canvasGroup.DOFade(0, _fadeTime);
    }

    public void FadeOut()
    {
        var color = _canvasGroup.color;
        color.a = 0;
        _canvasGroup.DOFade(1, _fadeTime).OnComplete(MoveScene);
    }

    void MoveScene()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
        _sceneLoader.LoadSceneByName(_sceneName);  // <-
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h������Canvas")] Image _canvasGroup;
    [SerializeField, Header("�t�F�[�h���鎞��")] float _fadeTime;
    [SerializeField, Header("�J�ڂ��������V�[����")] string _sceneName;

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
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
    }
}

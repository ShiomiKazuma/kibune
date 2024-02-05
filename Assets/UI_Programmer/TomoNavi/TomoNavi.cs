using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Tweening;
using DG;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
// ver 1 菅沼
//
public class TomoNavi : MonoBehaviour   // v-1
{
    enum Behaviour
    {
        DontDestroyOnComplete,
        DestroyOnComplete,
    }

    enum CallbackBehaviour
    {
        CallBack,
        None,
    }

    [SerializeField]
    float _displayingTime;
    [SerializeField]
    float _fadeOutDuration;
    [SerializeField]
    Behaviour OnCompleted;
    [SerializeField]
    CallbackBehaviour OnCompletedCallback;
    [SerializeField]
    int _callingObjectIndex;
    [SerializeField]
    UITween _uiTween;
    [SerializeField]
    Image _tomoNaviIcon;
    [SerializeField]
    Image _tomoNaviBG;
    [SerializeField]
    Text _tomoNaviText;
    [SerializeField, Header("消えるときに呼び出すイベント")]
    UnityEvent _onDestroyed;

    TomoNaviManager _tomoMan;

    GameManager _gameManager;

    void OnGameOver()
    {
        GameObject.Destroy(transform.parent.gameObject);
    }

    public void OnTweenEndStartFade()   // フェードアウト
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(_displayingTime);

        _tomoNaviText.DOFade(0f, _fadeOutDuration * .5f);
        _tomoNaviIcon.DOFade(0f, _fadeOutDuration * .75f);
        _tomoNaviBG.DOFade(0f, _fadeOutDuration).OnComplete(
            () =>
            {
                switch (OnCompleted)
                {
                    case Behaviour.DestroyOnComplete:
                        switch (OnCompletedCallback)
                        {
                            case CallbackBehaviour.CallBack:
                                _tomoMan.PopNavi(_callingObjectIndex);
                                break;
                            default: break;
                        }
                        _onDestroyed.Invoke();
                        GameObject.Destroy(transform.parent.gameObject);
                        break;
                    default:
                        _onDestroyed.Invoke();
                        break;
                }
            }
            );
    }

    private void Awake()
    {
        _gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void OnEnable()
    {
        _gameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _gameManager.OnGameOver -= OnGameOver;
    }

    private void Start()
    {
        _tomoMan = GameObject.FindFirstObjectByType<TomoNaviManager>();
        _uiTween.StartTween();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Tweening;
using DG;
using DG.Tweening;
using UnityEngine.UI;
// ver 1 菅沼
//
public class TomoNavi : MonoBehaviour   // v-1
{
    [SerializeField]
    float _displayingTime;
    [SerializeField]
    float _fadeOutDuration;
    [SerializeField]
    Behaviour OnCompleted;
    [SerializeField]
    UITween _uiTween;
    [SerializeField]
    Image _tomoNaviIcon;
    [SerializeField]
    Image _tomoNaviBG;
    [SerializeField]
    Text _tomoNaviText;

    enum Behaviour
    {
        DontDestroyOnComplete,
        DestroyOnComplete,
    }

    private void Start()
    {
        _uiTween.StartTween();
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
                    case Behaviour.DestroyOnComplete: GameObject.Destroy(transform.parent.gameObject); break;
                        default: break;
                }
            }
            );
    }
}

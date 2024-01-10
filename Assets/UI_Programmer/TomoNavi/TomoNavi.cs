using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLib.Tweening;
// ver 1 êõè¿
//
public class TomoNavi : MonoBehaviour   // v-1
{
    [SerializeField]
    float _timeInterval;
    [SerializeField]
    UITween _uiTween;

    private void Start()
    {
        _uiTween.StartTween();
    }

    private void FixedUpdate()
    {
        if (_uiTween != null)
        {

        }
    }
}

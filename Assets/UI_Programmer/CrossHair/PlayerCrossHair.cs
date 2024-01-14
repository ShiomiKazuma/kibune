using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
// 担当 ： UIプログラマ 
// ver 1 菅沼
public class PlayerCrossHair : MonoBehaviour
{
    [SerializeField]
    GameObject _imgTop;
    [SerializeField]
    Image _imgBtmLeft;
    [SerializeField]
    Image _imgBtmRight;
    [SerializeField]
    RectTransform _rectMiddleAnchor;
    [SerializeField]
    RectTransform _rectClosedLeft;
    [SerializeField]
    RectTransform _rectDeployedLeft;
    [SerializeField]
    RectTransform _rectClosedRight;
    [SerializeField]
    RectTransform _rectDeployedRight;

    public enum CrossHairStatus
    {
        Closed,
        Deployed,
    }

    bool _canGrapple;
    public bool CanGrapple => _canGrapple;
    bool _isGrappling;
    public bool IsGrappling => _isGrappling;

    public void SetGrappling(bool isGrappling) => _imgTop.SetActive(isGrappling);

    
}

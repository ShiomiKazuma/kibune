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
        Close,
        Deploy,
    }

    bool _canGrapple;
    public bool CanGrapple => _canGrapple;
    bool _isGrappling;
    public bool IsGrappling => _isGrappling;

    public void SetGrappling(bool isGrappling) => _imgTop.SetActive(isGrappling);

    public void SetCrossHairStatus(CrossHairStatus crossHairStatus)
    {
        switch (crossHairStatus)
        {
            case CrossHairStatus.Close:
                _imgBtmLeft.rectTransform.localPosition = _rectClosedLeft.localPosition;
                _imgBtmRight.rectTransform.localPosition = _rectClosedRight.localPosition;
                break;
            case CrossHairStatus.Deploy:
                _imgBtmLeft.rectTransform.localPosition = _rectDeployedLeft.localPosition;
                _imgBtmRight.rectTransform.localPosition = _rectDeployedRight.localPosition;
                break;
        }
    }

    public void SetCrossHairStatus(string crossHairStatus)
    {
        Vector2 endvalue_left = _imgBtmLeft.rectTransform.localPosition;
        Vector2 endvalue_right = _imgBtmRight.rectTransform.localPosition;
        switch (crossHairStatus)
        {
            case "Close":
                _imgBtmLeft.rectTransform.localPosition = _rectClosedLeft.localPosition;
                _imgBtmRight.rectTransform.localPosition = _rectClosedRight.localPosition;
                break;
            case "Deploy":
                _imgBtmLeft.rectTransform.localPosition = _rectDeployedLeft.localPosition;
                _imgBtmRight.rectTransform.localPosition = _rectDeployedRight.localPosition;
                break;
        }
    }
}

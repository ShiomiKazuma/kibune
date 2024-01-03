using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 担当 ： UIプログラマ 
// ver 1 菅沼
public class PlayerCrossHair : MonoBehaviour
{
    [SerializeField]
    GameObject _imgTop;
    [SerializeField]
    GameObject _imgBottomDeployed;
    [SerializeField]
    GameObject _imgBottomClosed;

    [SerializeField]
    bool _canGrapple;
    [SerializeField]
    bool _isGrappling;

    void SetCrossHairStatus(bool canGrapple, bool isGrappling)
    {
        _imgTop.SetActive(isGrappling);

        _imgBottomDeployed.SetActive(!canGrapple);
        _imgBottomClosed.SetActive(canGrapple);
    }

    private void FixedUpdate()
    {
        SetCrossHairStatus(_canGrapple, _isGrappling);
    }
}

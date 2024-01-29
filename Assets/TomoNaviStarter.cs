using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomoNaviStarter : MonoBehaviour
{
    [SerializeField]
    GameObject _tomoNavi;

    private void Start()
    {
        GameObject go = GameObject.Instantiate(_tomoNavi);
        TomoNaviManager manager = FindAnyObjectByType<TomoNaviManager>();
        go.transform.parent = manager.gameObject.transform;
        go.transform.localPosition = Vector3.zero;
    }
}

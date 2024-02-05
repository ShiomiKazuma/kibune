using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomoNaviStarter : MonoBehaviour
{
    [SerializeField]
    GameObject _tomoNavi;

    private void Start()
    {
        var hoge = GameObject.FindFirstObjectByType<FramedEventsInGameGeneralManager>();
        var data = hoge.ReadSaveData();
        if (!data.Finished[0])
        {
            GameObject go = GameObject.Instantiate(_tomoNavi);
            TomoNaviManager manager = FindAnyObjectByType<TomoNaviManager>();
            go.transform.parent = manager.gameObject.transform;
            go.transform.localPosition = Vector3.zero;
        }
    }
}

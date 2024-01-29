using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 作成 菅沼
public class TomoNaviManager : MonoBehaviour
{
    [SerializeField, Header("ポップさせる友ナビのオブジェクトをここにアタッチ")] List<GameObject> _objects;

    GameInfo _ginfo;

    private void Start()
    {
        _ginfo = GameObject.FindFirstObjectByType<GameInfo>();

        if (_ginfo.SceneStatus == GameInfo.SceneTransitStatus.To_InGameScene)
        {
            PopNavi(0);
        }
    }

    public void PopNavi(int index)
    {
        GameObject go = GameObject.Instantiate(_objects[index]);
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 作成 菅沼
public class TomoNaviManager : MonoBehaviour
{
    [SerializeField, Header("ポップさせる友ナビのオブジェクトをここにアタッチ")] List<GameObject> _objects;

    public void PopNavi(int index)
    {
        GameObject go = GameObject.Instantiate(_objects[index]);
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
    }
}

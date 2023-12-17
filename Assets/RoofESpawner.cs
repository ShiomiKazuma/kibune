using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 作成者 菅沼 
public class RoofESpawner : MonoBehaviour
{
    [SerializeField,
        Header("出現させる数")]
    uint _maxAmount = 3;
    [SerializeField,
        Header("出現させるオブジェクト")]
    GameObject _enemyObject;
    [SerializeField,
        Header("出現させるトランスフォーム（座標）")]
    Transform _popPosition;
    [SerializeField,
        Header("機能のテストをしているか？")]
    bool _debuggingFunction;
    [SerializeField,
        Header("プレイヤーキャラのタグ")]
    string _playerTag;

    Stack<GameObject> _stackedObj = new Stack<GameObject>();

    /// <summary> オブジェクトプール機能を利用するのに最初にこれを呼び出す。スタックにオブジェクトを登録する </summary>
    void SetUpPooling()
    {
        for (int i = 0; i < _maxAmount; i++)
        {
            _stackedObj.Push(_enemyObject);
        } // スタックに追加
    }

    /// <summary> スタックに登録されているオブジェクトをすべて一気にスポーンさせる </summary>
    void StageObjects()
    {
        for (int i = 0; i < _maxAmount; i++)
        {
            PopObjectAt(_popPosition);
        }
    }

    /// <summary> スタックにオブジェクトをプールする </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        if (_stackedObj.Count < _maxAmount)
        {
            _stackedObj.Push(obj);
        }
    }

    /// <summary> スタックの一番上のオブジェクトを返し、スタックからポップする </summary>
    /// <returns></returns>
    public GameObject PopObj()
    {
        GameObject tmp = null;
        if (_stackedObj.Count > 0)
        {
            tmp = _stackedObj.Peek();
            _stackedObj.Pop();
        }
        else
        {
            return null;
        }
        return tmp;
    }

    /// <summary> 位置を指定してのポップ </summary>
    /// <param name="transform"></param>
    public void PopObjectAt(Transform transform)
    {
        var pObj = PopObj();
        if (pObj != null)
        {
            pObj.transform.position = transform.position;
            GameObject.Instantiate(pObj);
        }
    }

    private void Start()
    {
        SetUpPooling();
        if (_debuggingFunction)
        {
            StageObjects();
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            StageObjects();
        }
    }
}

//////////////////////
///管理者：塩見和真///
/////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Doroon : MonoBehaviour
{
    [SerializeField, Header("爆発エフェクトのゲームオブジェクト")]
    GameObject _explosionGameObject;
    [SerializeField, Header("索敵範囲"), Range(0, 100)]
    float _sarchRange = 10;
    [SerializeField, Header("凝視からチェイスまでの時間"), Range(0, 10)]
    float _lookTime = 2;
    [SerializeField, Header("ドローンのスピード")]
    float _doroonSpeed = 10;
    [SerializeField, Header("ドローンのステート")]
    State _state;
    [SerializeField, Header("画像データを入れる用のゲームオブジェクト")]
    GameObject _imageObject;
    [SerializeField, Header("？マーク")]
    Image _hatena;
    [SerializeField, Header("!マーク")]
    Image _biltukuri;
    Image _image;
    [SerializeField, Header("自爆するまでの時間")]
    float _explosionTime;
    float _explosionTimer;
    bool IsChase = false;
    //プレイヤーのゲームオブジェクト
    GameObject _player;
    //現在の凝視時間
    float _lookTimer;
    //プレイヤーのレイヤーマスク
    LayerMask _layerMask = 1 << 7;
    Rigidbody _rb;
    public enum State
    {
        Serch,
        Look,
        Chase
    }

    // Start is called before the first frame update
    void Start()
    {
        _lookTimer = 0;
        _explosionTimer = 0;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        _state = State.Serch;
        _image = _imageObject.GetComponent<Image>();
        _rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == State.Serch)
        {
            Serch();
        }    
        else if (_state == State.Look)
        {
            Look();
        }
        else if(_state == State.Chase)
        {
            Chase();
        }
    }

    /// <summary> サーチ状態の処理 </summary>
    private void Serch()
    {
        Debug.Log("探しています");
        //回転をリセットする
        this.transform.rotation = Quaternion.identity;
        //索敵範囲内にプレイヤーがいる場合の処理
        if(Vector3.Distance(this.transform.position, _player.gameObject.transform.position) <= _sarchRange)
        {
            RaycastHit hit;
            Vector3 dir = (_player.transform.position - this.transform.position).normalized;
            if (Physics.Raycast(this.transform.position, dir, out hit, _sarchRange, _layerMask))
            {
                _state = State.Look;
                _image = _hatena;
            }  
        }
    }

    /// <summary> 凝視時の処理 </summary>
    void Look()
    {
        Debug.Log("凝視しています");
        //プレイヤーの方向に向く
        Vector3 dir = (_player.transform.position - this.transform.position).normalized;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, dir, out hit, _sarchRange, _layerMask))
        {
            //凝視タイマーを増やしていく
            _lookTimer += Time.deltaTime;
            //凝視タイマーを超えていたら、追跡モードに移行する
            if (_lookTimer > _lookTime)
            {
                StartCoroutine("ChaseWait");
                _state = State.Chase;
                _image = _biltukuri;  
            }
        }
        else
        {
            //凝視時間をリセットする
            _lookTimer = 0;
            _image = null;
            _state = State.Serch;
        }        
    }

    /// <summary> 突撃時の処理 </summary>
    IEnumerable ChaseWait()
    {
        Debug.Log("突撃準備");
        //1秒数を待つ
        yield return new WaitForSeconds(1.0f);
        IsChase = true;
    }

    /// <summary> 突撃時の処理 </summary>
    void Chase()
    {
        if(IsChase)
        {
            _explosionTimer += Time.deltaTime;
            Debug.Log("追跡開始");
            //プレイヤーを追う
            Vector3 dir = (_player.transform.position - this.transform.position).normalized;
            Quaternion quaternion = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
            _rb.AddForce(dir * _doroonSpeed);
            //自爆時間を過ぎるかプレイヤーに近づくと爆発する
            if (_explosionTime < _explosionTimer || Vector3.Distance(this.transform.position, _player.transform.position) < 1)
            {
                Instantiate(_explosionGameObject, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }  
}

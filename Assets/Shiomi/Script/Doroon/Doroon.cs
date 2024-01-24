//////////////////////
///管理者：塩見和真///
/////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    float _doroonSpeed = 1;
    [SerializeField, Header("ドローンの速度制限")]
    float _doroonLimitSpeed = 5;
    [SerializeField, Header("ドローンのステート")]
    State _state;
    [SerializeField, Header("画像データを入れる用のゲームオブジェクト")]
    Image _image;       //  <-
    [SerializeField, Header("？マーク")]
    Sprite _hatena;
    [SerializeField, Header("!マーク")]
    Sprite _biltukuri;
    [SerializeField, Header("自爆するまでの時間")]
    float _explosionTime;
    [SerializeField, Header("どこまで近づけば爆発するか")]
    float _explosionPos = 2f;
    float _explosionTimer;
    bool IsChase = false;
    //プレイヤーのゲームオブジェクト
    [SerializeField, Header("プレイヤー")] GameObject _player;
    //現在の凝視時間
    float _lookTimer;
    Rigidbody _rb;
    Collider _collider;

    public enum State
    {
        Serch,
        Look,
        Chase
    }

    private void Awake()
    {
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _lookTimer = 0;
        _explosionTimer = 0;
        _state = State.Serch;
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
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
        else if (_state == State.Chase)
        {
            Chase();
        }
    }

    /// <summary> サーチ状態の処理 </summary>
    private void Serch()
    {
        Debug.Log("探しています");
        //回転をリセットする
        //this.transform.rotation = Quaternion.identity;
        //索敵範囲内にプレイヤーがいる場合の処理
        if (Vector3.Distance(this.transform.position, _player.transform.position) < _sarchRange)
        {
            RaycastHit hit;
            Vector3 dir = (_player.transform.position - this.transform.position).normalized;
            if (Physics.Raycast(this.transform.position, dir, out hit, _sarchRange))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    _state = State.Look;
                    _image.sprite = _hatena;
                }
            }
        }
    }

    /// <summary> 凝視時の処理 </summary>
    void Look()
    {
        Debug.Log("凝視しています");
        //プレイヤーの方向を取得
        Vector3 dir = (_player.transform.position - this.transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, dir, out hit, _sarchRange))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //プレイヤーの方向に向く
                Quaternion quaternion = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
                //凝視タイマーを増やしていく
                _lookTimer += Time.deltaTime;
                // 画像のFill具合を更新
                _image.fillAmount = (_lookTimer / _lookTime);       // <-
                //凝視タイマーを超えていたら、追跡モードに移行する
                if (_lookTimer > _lookTime)
                {
                    //StartCoroutine("ChaseWait");
                    _state = State.Chase;
                    _image.sprite = _biltukuri;
                    IsChase = true;
                }
            }
            else if (!(hit.collider.gameObject.tag == "Player"))
            {
                //凝視時間をリセットする
                _lookTimer = 0;
                _image.sprite = null;
                _state = State.Serch;
            }
        }
        else
        {
            //凝視時間をリセットする
            _lookTimer = 0;
            _image.sprite = null;
            _state = State.Serch;
        }
    }

    /// <summary> 突撃時の処理 </summary>
    //IEnumerable ChaseWait()
    //{
    //    Debug.Log("突撃準備");
    //    //1秒数を待つ
    //    yield return new WaitForSeconds(1.0f);
    //    IsChase = true;
    //}

    /// <summary> 突撃時の処理 </summary>
    void Chase()
    {
        if (IsChase)
        {
            _collider.enabled = true;
            _explosionTimer += Time.deltaTime;
            Debug.Log("追跡開始");
            //プレイヤーを追う
            Vector3 dir = (_player.transform.position - this.transform.position).normalized;
            Quaternion quaternion = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
            _rb.AddForce(dir * _doroonSpeed);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _doroonLimitSpeed);
            //自爆時間を過ぎるかプレイヤーに近づくと爆発する
            if (_explosionTime < _explosionTimer || Vector3.Distance(this.transform.position, _player.transform.position) < _explosionPos)
            {
                Debug.Log("爆発");
                var dest = Instantiate(_explosionGameObject, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sarchRange);
    }
}

//////////////////////
///管理者：塩見和真///
/////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField, Header("爆風に当たったときに吹っ飛ぶ力の強さ")] 
    private float _futtobiPower;
    [SerializeField, Header("爆風の判定が実際に発生するまでのディレイ")]
    private float _startDelaySeconds = 0.1f;
    //[SerializeField, Header("爆風の持続フレーム数")] private int _durationFrameCount = 1;
    [SerializeField, Header("エフェクト含めすべての再生が終了するまでの時間")] private float _stopSeconds = 2f;
    [SerializeField, Header("パーティクルエフェクトを入れる")] private ParticleSystem _effect;
    [SerializeField, Header("プレイヤーのゲームオブジェクト")] GameObject _player;
    [SerializeField, Header("爆発の範囲")] float _explosionrange = 5f;
    [SerializeField, Header("爆風が当たるレイヤー")] LayerMask _layerMask;
    public void Awake()
    {
        _effect.Stop();
    }

    /// <summary> 呼び出された時に爆発する </summary>
    private void Start()
    {
        // 当たり判定管理のコルーチン
        StartCoroutine(ExplodeCoroutine());
        // 爆発エフェクト含めてもろもろを消すコルーチン
        StartCoroutine(StopCoroutine());
        // エフェクト
        _effect.Play();
    }

    IEnumerator ExplodeCoroutine()
    {
        // 指定秒数が経過するまでFixedUpdate上で待つ
        var delayCount = Mathf.Max(0, _startDelaySeconds);
        while (delayCount > 0)
        {
            yield return new WaitForFixedUpdate();
            delayCount -= Time.fixedDeltaTime;
        }
        //時間が経過したらあたり判定を取って来る

        //ここから下は、失敗したもの
        //Vector3 dir = (_player.transform.position - this.transform.position).normalized;
        //if(Physics.SphereCastAll(this.transform.position, _explosionrange, _explosionrange, out hit))
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, _explosionrange, transform.forward, 0f, _layerMask, QueryTriggerInteraction.UseGlobal);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //var rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                ////リジッドボディがあるなら吹っ飛ばす
                //if(rb != null)
                //{
                //    rb.AddForce(dir * _futtobiPower, ForceMode.VelocityChange);
                //}
                //死亡処理をする
                Debug.Log("Dead");
            }
        }
        //ここまでが失敗したもの

        yield return new WaitForFixedUpdate();
        
    }

    IEnumerator StopCoroutine()
    {
        // 時間経過後に消す
        yield return new WaitForSeconds(_stopSeconds);
        _effect.Stop();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionrange);
    }
}

//////////////////////
///�Ǘ��ҁF�����a�^///
/////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField, Header("�����ɓ��������Ƃ��ɐ�����ԗ͂̋���")] 
    private float _futtobiPower;
    [SerializeField, Header("�����̔��肪���ۂɔ�������܂ł̃f�B���C")]
    private float _startDelaySeconds = 0.1f;
    //[SerializeField, Header("�����̎����t���[����")] private int _durationFrameCount = 1;
    [SerializeField, Header("�G�t�F�N�g�܂߂��ׂĂ̍Đ����I������܂ł̎���")] private float _stopSeconds = 2f;
    [SerializeField, Header("�p�[�e�B�N���G�t�F�N�g������")] private ParticleSystem _effect;
    [SerializeField, Header("�v���C���[�̃Q�[���I�u�W�F�N�g")] GameObject _player;
    [SerializeField, Header("�����͈̔�")] float _explosionrange = 5f;
    [SerializeField, Header("�����������郌�C���[")] LayerMask _layerMask;
    public void Awake()
    {
        _effect.Stop();
    }

    /// <summary> �Ăяo���ꂽ���ɔ������� </summary>
    private void Start()
    {
        // �����蔻��Ǘ��̃R���[�`��
        StartCoroutine(ExplodeCoroutine());
        // �����G�t�F�N�g�܂߂Ă������������R���[�`��
        StartCoroutine(StopCoroutine());
        // �G�t�F�N�g
        _effect.Play();
    }

    IEnumerator ExplodeCoroutine()
    {
        // �w��b�����o�߂���܂�FixedUpdate��ő҂�
        var delayCount = Mathf.Max(0, _startDelaySeconds);
        while (delayCount > 0)
        {
            yield return new WaitForFixedUpdate();
            delayCount -= Time.fixedDeltaTime;
        }
        //���Ԃ��o�߂����炠���蔻�������ė���

        //�������牺�́A���s��������
        //Vector3 dir = (_player.transform.position - this.transform.position).normalized;
        //if(Physics.SphereCastAll(this.transform.position, _explosionrange, _explosionrange, out hit))
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, _explosionrange, transform.forward, 0f, _layerMask, QueryTriggerInteraction.UseGlobal);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //var rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                ////���W�b�h�{�f�B������Ȃ琁����΂�
                //if(rb != null)
                //{
                //    rb.AddForce(dir * _futtobiPower, ForceMode.VelocityChange);
                //}
                //���S����������
                Debug.Log("Dead");
            }
        }
        //�����܂ł����s��������

        yield return new WaitForFixedUpdate();
        
    }

    IEnumerator StopCoroutine()
    {
        // ���Ԍo�ߌ�ɏ���
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

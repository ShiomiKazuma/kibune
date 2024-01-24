//////////////////////
///�Ǘ��ҁF�����a�^///
/////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doroon : MonoBehaviour
{
    [SerializeField, Header("�����G�t�F�N�g�̃Q�[���I�u�W�F�N�g")]
    GameObject _explosionGameObject;
    [SerializeField, Header("���G�͈�"), Range(0, 100)]
    float _sarchRange = 10;
    [SerializeField, Header("�Î�����`�F�C�X�܂ł̎���"), Range(0, 10)]
    float _lookTime = 2;
    [SerializeField, Header("�h���[���̃X�s�[�h")]
    float _doroonSpeed = 1;
    [SerializeField, Header("�h���[���̑��x����")]
    float _doroonLimitSpeed = 5;
    [SerializeField, Header("�h���[���̃X�e�[�g")]
    State _state;
    [SerializeField, Header("�摜�f�[�^������p�̃Q�[���I�u�W�F�N�g")]
    Image _image;       //  <-
    [SerializeField, Header("�H�}�[�N")]
    Sprite _hatena;
    [SerializeField, Header("!�}�[�N")]
    Sprite _biltukuri;
    [SerializeField, Header("��������܂ł̎���")]
    float _explosionTime;
    [SerializeField, Header("�ǂ��܂ŋ߂Â��Δ������邩")]
    float _explosionPos = 2f;
    float _explosionTimer;
    bool IsChase = false;
    //�v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField, Header("�v���C���[")] GameObject _player;
    //���݂̋Î�����
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

    /// <summary> �T�[�`��Ԃ̏��� </summary>
    private void Serch()
    {
        Debug.Log("�T���Ă��܂�");
        //��]�����Z�b�g����
        //this.transform.rotation = Quaternion.identity;
        //���G�͈͓��Ƀv���C���[������ꍇ�̏���
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

    /// <summary> �Î����̏��� </summary>
    void Look()
    {
        Debug.Log("�Î����Ă��܂�");
        //�v���C���[�̕������擾
        Vector3 dir = (_player.transform.position - this.transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, dir, out hit, _sarchRange))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //�v���C���[�̕����Ɍ���
                Quaternion quaternion = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
                //�Î��^�C�}�[�𑝂₵�Ă���
                _lookTimer += Time.deltaTime;
                // �摜��Fill����X�V
                _image.fillAmount = (_lookTimer / _lookTime);       // <-
                //�Î��^�C�}�[�𒴂��Ă�����A�ǐՃ��[�h�Ɉڍs����
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
                //�Î����Ԃ����Z�b�g����
                _lookTimer = 0;
                _image.sprite = null;
                _state = State.Serch;
            }
        }
        else
        {
            //�Î����Ԃ����Z�b�g����
            _lookTimer = 0;
            _image.sprite = null;
            _state = State.Serch;
        }
    }

    /// <summary> �ˌ����̏��� </summary>
    //IEnumerable ChaseWait()
    //{
    //    Debug.Log("�ˌ�����");
    //    //1�b����҂�
    //    yield return new WaitForSeconds(1.0f);
    //    IsChase = true;
    //}

    /// <summary> �ˌ����̏��� </summary>
    void Chase()
    {
        if (IsChase)
        {
            _collider.enabled = true;
            _explosionTimer += Time.deltaTime;
            Debug.Log("�ǐՊJ�n");
            //�v���C���[��ǂ�
            Vector3 dir = (_player.transform.position - this.transform.position).normalized;
            Quaternion quaternion = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
            _rb.AddForce(dir * _doroonSpeed);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _doroonLimitSpeed);
            //�������Ԃ��߂��邩�v���C���[�ɋ߂Â��Ɣ�������
            if (_explosionTime < _explosionTimer || Vector3.Distance(this.transform.position, _player.transform.position) < _explosionPos)
            {
                Debug.Log("����");
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

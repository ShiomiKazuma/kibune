//////////////////////
///�Ǘ��ҁF�����a�^///
/////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
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
    float _doroonSpeed = 10;
    [SerializeField, Header("�h���[���̃X�e�[�g")]
    State _state;
    [SerializeField, Header("�摜�f�[�^������p�̃Q�[���I�u�W�F�N�g")]
    GameObject _imageObject;
    [SerializeField, Header("�H�}�[�N")]
    Image _hatena;
    [SerializeField, Header("!�}�[�N")]
    Image _biltukuri;
    Image _image;
    [SerializeField, Header("��������܂ł̎���")]
    float _explosionTime;
    float _explosionTimer;
    bool IsChase = false;
    //�v���C���[�̃Q�[���I�u�W�F�N�g
    GameObject _player;
    //���݂̋Î�����
    float _lookTimer;
    //�v���C���[�̃��C���[�}�X�N
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

    /// <summary> �T�[�`��Ԃ̏��� </summary>
    private void Serch()
    {
        Debug.Log("�T���Ă��܂�");
        //��]�����Z�b�g����
        this.transform.rotation = Quaternion.identity;
        //���G�͈͓��Ƀv���C���[������ꍇ�̏���
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

    /// <summary> �Î����̏��� </summary>
    void Look()
    {
        Debug.Log("�Î����Ă��܂�");
        //�v���C���[�̕����Ɍ���
        Vector3 dir = (_player.transform.position - this.transform.position).normalized;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, dir, out hit, _sarchRange, _layerMask))
        {
            //�Î��^�C�}�[�𑝂₵�Ă���
            _lookTimer += Time.deltaTime;
            //�Î��^�C�}�[�𒴂��Ă�����A�ǐՃ��[�h�Ɉڍs����
            if (_lookTimer > _lookTime)
            {
                StartCoroutine("ChaseWait");
                _state = State.Chase;
                _image = _biltukuri;  
            }
        }
        else
        {
            //�Î����Ԃ����Z�b�g����
            _lookTimer = 0;
            _image = null;
            _state = State.Serch;
        }        
    }

    /// <summary> �ˌ����̏��� </summary>
    IEnumerable ChaseWait()
    {
        Debug.Log("�ˌ�����");
        //1�b����҂�
        yield return new WaitForSeconds(1.0f);
        IsChase = true;
    }

    /// <summary> �ˌ����̏��� </summary>
    void Chase()
    {
        if(IsChase)
        {
            _explosionTimer += Time.deltaTime;
            Debug.Log("�ǐՊJ�n");
            //�v���C���[��ǂ�
            Vector3 dir = (_player.transform.position - this.transform.position).normalized;
            Quaternion quaternion = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 0.1f);
            _rb.AddForce(dir * _doroonSpeed);
            //�������Ԃ��߂��邩�v���C���[�ɋ߂Â��Ɣ�������
            if (_explosionTime < _explosionTimer || Vector3.Distance(this.transform.position, _player.transform.position) < 1)
            {
                Instantiate(_explosionGameObject, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }  
}

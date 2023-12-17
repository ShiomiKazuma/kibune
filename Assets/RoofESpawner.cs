using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �쐬�� ���� 
public class RoofESpawner : MonoBehaviour
{
    [SerializeField,
        Header("�o�������鐔")]
    uint _maxAmount = 3;
    [SerializeField,
        Header("�o��������I�u�W�F�N�g")]
    GameObject _enemyObject;
    [SerializeField,
        Header("�o��������g�����X�t�H�[���i���W�j")]
    Transform _popPosition;
    [SerializeField,
        Header("�@�\�̃e�X�g�����Ă��邩�H")]
    bool _debuggingFunction;
    [SerializeField,
        Header("�v���C���[�L�����̃^�O")]
    string _playerTag;

    Stack<GameObject> _stackedObj = new Stack<GameObject>();

    /// <summary> �I�u�W�F�N�g�v�[���@�\�𗘗p����̂ɍŏ��ɂ�����Ăяo���B�X�^�b�N�ɃI�u�W�F�N�g��o�^���� </summary>
    void SetUpPooling()
    {
        for (int i = 0; i < _maxAmount; i++)
        {
            _stackedObj.Push(_enemyObject);
        } // �X�^�b�N�ɒǉ�
    }

    /// <summary> �X�^�b�N�ɓo�^����Ă���I�u�W�F�N�g�����ׂĈ�C�ɃX�|�[�������� </summary>
    void StageObjects()
    {
        for (int i = 0; i < _maxAmount; i++)
        {
            PopObjectAt(_popPosition);
        }
    }

    /// <summary> �X�^�b�N�ɃI�u�W�F�N�g���v�[������ </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        if (_stackedObj.Count < _maxAmount)
        {
            _stackedObj.Push(obj);
        }
    }

    /// <summary> �X�^�b�N�̈�ԏ�̃I�u�W�F�N�g��Ԃ��A�X�^�b�N����|�b�v���� </summary>
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

    /// <summary> �ʒu���w�肵�Ẵ|�b�v </summary>
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

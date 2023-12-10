using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] public Item _item;
    [SerializeField] public AcitiveType _acitiveType;
    [SerializeField] float _touchRange = 0.5f;
    GameObject _player;

    public abstract void Active();

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    //velocity�p
    //private void OnTriggerEnter(Collider collider)
    //{
    //    if(collider.gameObject.tag.Equals("Player"))
    //    {
    //        if(_acitiveType == AcitiveType.Use)
    //        {
    //            Active();
    //            Destroy(this.gameObject);
    //        }
    //        else if(_acitiveType == AcitiveType.PickUp)
    //        {
    //            //�����ɃC���x���g���ɓ���鏈��������
    //            Inventory._instance.Add(_item);
    //        }
    //    }
    //}

    //�L�����N�^�[�R���g���[���[�p
    private void Update()
    {
        if(!(_player == null))
        {
            if(Vector3.Distance(_player.transform.position, this.transform.position) < _touchRange)
            {
                if (_acitiveType == AcitiveType.Use)
                {
                    Active();
                    Destroy(this.gameObject);
                }
                else if (_acitiveType == AcitiveType.PickUp)
                {
                    //�����ɃC���x���g���ɓ���鏈��������
                    bool wasPickUp = Inventory._instance.Add(_item);
                    if(wasPickUp)
                        Destroy(this.gameObject);
                }   
            }
        }
    }

    public enum AcitiveType
    {
        Use,
        PickUp,
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _touchRange);
    }
}

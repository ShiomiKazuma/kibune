using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField, Header("�A�C�e��ID")] int _id;
    [SerializeField, Header("FlagManager�����Ă���Q�[���I�u�W�F�N�g")] FlagManager _flagManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _flagManager.AddItem(_id);
        }
    }
}

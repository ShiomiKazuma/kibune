using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField, Header("アイテムID")] int _id;
    [SerializeField, Header("FlagManagerがついているゲームオブジェクト")] FlagManager _flagManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _flagManager.AddItem(_id);
        }
    }
}

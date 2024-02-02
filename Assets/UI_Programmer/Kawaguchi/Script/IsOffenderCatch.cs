using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsOffenderCatch : MonoBehaviour
{
    [Header("車の上に乗った時の処理")]
    [SerializeField] UnityEvent _event;
    [SerializeField, Header("Player Layer")]
    LayerMask layerMask;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == layerMask)
        _event.Invoke();
    }
}

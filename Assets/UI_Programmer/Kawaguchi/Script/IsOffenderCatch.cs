using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsOffenderCatch : MonoBehaviour
{
    [Header("�Ԃ̏�ɏ�������̏���")]
    [SerializeField] UnityEvent _event;

    private void OnCollisionEnter(Collision collision)
    {

        _event.Invoke();
    }
}

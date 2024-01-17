using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsOffenderCatch : MonoBehaviour
{
    [Header("車の上に乗った時の処理")]
    [SerializeField] UnityEvent _event;

    private void OnCollisionEnter(Collision collision)
    {

        _event.Invoke();
    }
}

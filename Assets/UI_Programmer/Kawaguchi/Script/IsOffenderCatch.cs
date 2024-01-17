using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsOffenderCatch : MonoBehaviour
{
    [Header("Ô‚Ìã‚Éæ‚Á‚½‚Ìˆ—")]
    [SerializeField] UnityEvent _event;

    private void OnCollisionEnter(Collision collision)
    {

        _event.Invoke();
    }
}

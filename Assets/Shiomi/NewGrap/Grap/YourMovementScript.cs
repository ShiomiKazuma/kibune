using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourMovementScript : MonoBehaviour
{
    public Rigidbody _rb;
    public bool _freeze;
    // Update is called once per frame
    void Update()
    {
        if (_freeze)
            _rb.velocity = Vector3.zero;
    }
}

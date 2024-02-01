using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField, Header("カメラの原点のポジションになるものをアタッチ")]
    public Transform _cameraPosition;

    private void Update()
    {
        transform.position = _cameraPosition.position;
    }
}

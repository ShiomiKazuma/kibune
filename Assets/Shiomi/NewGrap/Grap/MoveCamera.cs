using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField, Header("�J�����̌��_�̃|�W�V�����ɂȂ���̂��A�^�b�`")]
    public Transform _cameraPosition;

    private void Update()
    {
        transform.position = _cameraPosition.position;
    }
}

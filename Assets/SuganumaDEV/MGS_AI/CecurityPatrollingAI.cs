using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CecurityPatrollingAI : MonoBehaviour
{
    [SerializeField, Range(0,180f)] float _fSightRange;
    [SerializeField] Transform _tLookAtTarget;
    [SerializeField] Transform _tTarget;
    [SerializeField] float _fSightDistanceLimit;

    private void Update()
    {
        if (CaplureNow())
        {
            Debug.Log("Captured!");
        }
    }

    bool CaplureNow()
    {
        Vector3 vLook = _tLookAtTarget.position - transform.position;
        Vector3 vTarget = _tTarget.position - transform.position;
        float fCosHalfSight = Mathf.Cos(_fSightRange / 2 * Mathf.Deg2Rad);
        float fCosTarget = Vector3.Dot(vLook, vTarget) / (vLook.magnitude * vTarget.magnitude);
        return fCosTarget > fCosHalfSight && vTarget.magnitude < _fSightDistanceLimit;
    }

    void OnDrawGizmos()
    {
        // ���E�͈̔́i���ʋy�э��E�̒[�j���M�Y���Ƃ��ĕ`��
        Vector3 lookAtDirection = (_tLookAtTarget.position - this.transform.position).normalized;    // ���ʁi���K���j
        Vector3 rightBorder = Quaternion.Euler(0, _fSightRange / 2, 0) * lookAtDirection;    // �E�[�i���K���j
        Vector3 leftBorder = Quaternion.Euler(0, -1 * _fSightRange / 2, 0) * lookAtDirection;    // ���[�i���K���j
        Gizmos.color = Color.cyan;  // ���ʂ͐��F�ŕ`��
        Gizmos.DrawRay(this.transform.position, lookAtDirection * _fSightDistanceLimit);
        Gizmos.color = Color.blue;  // ���[�͐ŕ`��
        Gizmos.DrawRay(this.transform.position, rightBorder * _fSightDistanceLimit);
        Gizmos.DrawRay(this.transform.position, leftBorder * _fSightDistanceLimit);
        // ��`�������̓R�[����̐}�`�� gizmo ���邢�̓Q�[����ɕ\���������ꍇ�́A
        // Mesh �𓮓I�ɐ������� Gizmos.DrawMesh ���g��
    }
}

using SLib.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SLib.SLib;

// �쐬 ����
public class RunningVeicleState : MonoBehaviour
{
    #region __DEBUG__
    bool __DEBUG__ = false;
    #endregion

    // �e�K�v�p�����[�^
    Transform _selfTransform;

    NavMeshAgent _agent;

    List<Vector3> _patrolPath = new();

    int _currentPathIndex;

    public RunningVeicleState(NavMeshAgent agent, Transform selfTransform, PathHolder patrollingPath)
    {
        _agent = agent;
        _selfTransform = selfTransform;
        foreach (var path in patrollingPath.GetPatrollingPath())
        {
            _patrolPath.Add(path);
        }
    }

    public void UpdateSelf(Transform selfTransform)
    {
        _selfTransform = selfTransform;
    }

    public void Entry()
    {
        Knock(__DEBUG__,
        () => Debug.Log("������n�߂�I"));
        _agent.SetDestination(_selfTransform.position);
    }

    public void Update()
    {
        Knock(__DEBUG__,
            () => Debug.Log("����"));
        if ((_selfTransform.position - _patrolPath[_currentPathIndex]).sqrMagnitude < 9)
        {
            _currentPathIndex = (_currentPathIndex < _patrolPath.Count - 1) ? _currentPathIndex + 1 : 0;
        }
        else
        {
            _agent.SetDestination(_patrolPath[_currentPathIndex]);
        }
    }

    public void Exit()
    {
        Knock(__DEBUG__,
        () => Debug.Log("������I���I"));
    }
}
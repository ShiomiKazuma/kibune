using RSEngine.AI.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary> Wanted AI State : Default </summary>
/// �f�t�H���g�ł͓���̌o�H���p�g���[������B
public class WantedAIStateDefault : IState
{
    // �e�K�v�p�����[�^
    int _currentPathIndex;
    Transform[] _patrollingPath;
    Transform _selfTransform;

    NavMeshAgent _agent;
    public WantedAIStateDefault(Transform[] patrollingPath, NavMeshAgent agent)
    {
        _agent = agent;
        _patrollingPath = patrollingPath;
        _currentPathIndex = 0;
    }
    public void Update(Transform selfTransform)
    {
        _selfTransform = selfTransform;
    }
    void Patroll()
    {
        if (_patrollingPath != null && _patrollingPath.Length > 0)
        {
            var isNearToPoint = (_patrollingPath[_currentPathIndex].position - _selfTransform.position).sqrMagnitude < 2;
            if(isNearToPoint)
            {
                _currentPathIndex = (_currentPathIndex + 1 < _patrollingPath.Length) ? _currentPathIndex + 1 : 0;
            }
            _agent.SetDestination(_patrollingPath[_currentPathIndex].position);
        }
    }
    public void Do()
    {
        Debug.Log("����");
        Patroll();
    }

    public void In()
    {
        Debug.Log("������n�߂�I");
    }

    public void Out()
    {
        Debug.Log("������I���I");
    }
}

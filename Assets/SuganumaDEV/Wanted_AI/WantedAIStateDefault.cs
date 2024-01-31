// 管理者 菅沼
using UnityEngine;
using SLib.StateSequencer;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using SLib.AI;
using static SLib.SLib;
using UnityEngine.UI;

/// <summary> Wanted AI State : Default </summary>
/// デフォルトでは特定の経路をパトロールする。
public class WantedAIStateDefault : IState
{
    #region __DEBUG__
    bool __DEBUG__ = false;
    #endregion

    // 各必要パラメータ
    Transform _selfTransform;

    NavMeshAgent _agent;

    List<Vector3> _patrolPath = new();

    Image _image;

    Action CB;

    int _currentPathIndex;

    public WantedAIStateDefault(NavMeshAgent agent, Transform selfTransform, PathHolder patrollingPath, Image image, Action callBack)
    {
        _agent = agent;
        _selfTransform = selfTransform;
        _image = image;
        CB = callBack;
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
        () => Debug.Log("巡回を始める！"));
        _agent.SetDestination(_selfTransform.position);
    }

    public void Update()
    {
        Knock(__DEBUG__,
            () => Debug.Log("巡回中"));
        _image.color = Color.clear;
        CB();
        if ((_selfTransform.position - _patrolPath[_currentPathIndex]).sqrMagnitude < 2)
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
        () => Debug.Log("巡回を終わる！"));
    }
}
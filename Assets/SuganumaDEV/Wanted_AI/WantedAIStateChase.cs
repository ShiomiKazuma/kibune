// ä«óùé“ êõè¿
using SLib.StateSequencer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static SLib.SLib;
/// <summary> Wanted AI State : Chase </summary>
public class WantedAIStateChase : IState
{
    #region __DEBUG__
    bool __DEBUG__ = false;
    #endregion

    float _sightRange;
    LayerMask _targetLayer;
    Transform _selfTransform;
    NavMeshAgent _agent;
    Action CB;
    public WantedAIStateChase(float sightRange, LayerMask targetLayer, Transform selfTransform, NavMeshAgent agent, Action callBack)
    {
        _sightRange = sightRange;
        _targetLayer = targetLayer;
        _selfTransform = selfTransform;
        _agent = agent;
        CB = callBack;
    }

    public void UpdateSelf(Transform selfTransform)
    {
        _selfTransform = selfTransform;
    }

    public void Entry()
    {
        Knock(__DEBUG__,
        () => Debug.Log("í«Ç§ÇºÅI"));
        CB();
    }

    public void Update()
    {
        Knock(__DEBUG__, 
        () => Debug.Log("Ç‹ÇƒÅI"));
        if (Physics.CheckSphere(_selfTransform.position, _sightRange, _targetLayer))
        {
            var cols = Physics.OverlapSphere(_selfTransform.position, _sightRange, _targetLayer);
            _agent.SetDestination(cols[0].transform.position);
        }
        else
        {
            _agent.SetDestination(_selfTransform.position);
        }
    }

    public void Exit()
    {
        Knock(__DEBUG__,
        ()=> Debug.Log("Ç‡Ç§í«ÇÌÇ»Ç¢"));
    }
}

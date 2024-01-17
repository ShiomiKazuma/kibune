using SLib.AI;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class RunningGuilty : MonoBehaviour
{
    [SerializeField]
    PathHolder _runningPath;

    RunningVeicleState _runningState;
    NavMeshAgent _agent;

    bool _isActivated;
    bool _isRunning;

    public void Activate()
    {
        if (!_isActivated)
        {
            _agent = GetComponent<NavMeshAgent>();
            _isActivated = true;
            _runningState = new RunningVeicleState(_agent, transform, _runningPath);
            _runningState.Entry();
            _isRunning = true;
        }
    }

    public void Deactivate()
    {
        if (_isRunning)
        {
            _isRunning = false;
            _isActivated = false;
            _agent.SetDestination(transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (_isActivated)
        {
            _runningState.UpdateSelf(transform);
            _runningState.Update();
        }
    }
}

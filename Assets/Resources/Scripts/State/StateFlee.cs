using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateFlee : IState
{
    private StateManager stateManager;
    private NavMeshAgent nav;
    private Transform playerTr;

    public StateFlee(StateManager _stateManager, NavMeshAgent _nav, Transform _playerTr)
    {
        stateManager = _stateManager;
        nav = _nav;
        playerTr = _playerTr;
    }

    public void OnEnter()
    {
        Vector3 fleeDirection = (stateManager.transform.position - playerTr.position).normalized * 10;
        Vector3 fleePosition = stateManager.transform.position + fleeDirection;
        nav.SetDestination(fleePosition);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}


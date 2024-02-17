using UnityEngine;
using UnityEngine.AI;

public class StateChase : IState
{
    private StateManager stateManager;
    private NavMeshAgent nav;
    private Transform playerTr;

    public StateChase(StateManager _stateManager, NavMeshAgent _nav, Transform _playerTr)
    {
        stateManager = _stateManager;
        nav = _nav;
        playerTr = _playerTr;
    }

    public void OnEnter()
    {
        nav.SetDestination(playerTr.position);
    }

    public void OnUpdate()
    {
        nav.SetDestination(playerTr.position);
    }

    public void OnExit()
    {

    }
}

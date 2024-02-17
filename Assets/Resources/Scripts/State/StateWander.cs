using UnityEngine;
using UnityEngine.AI;

public class StateWander : IState
{
    private StateManager stateManager;
    private NavMeshAgent nav;
    private Animator anim;
    private Transform playerTr;

    public StateWander(StateManager _stateManager, NavMeshAgent _nav, Animator _anim, Transform _playerTr)
    {
        stateManager = _stateManager;
        nav = _nav;
        anim = _anim;
        playerTr = _playerTr;
    }

    public void OnEnter()
    {
        anim.SetBool("IsMoving", true);
        nav.SetDestination(GetRandomDestination());
    }

    public void OnUpdate()
    {
        if (nav.remainingDistance < 0.5f)
        {
            nav.SetDestination(GetRandomDestination());
        }
    }

    public void OnExit()
    {
        anim.SetBool("IsMoving", false);
    }

    private Vector3 GetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += stateManager.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10, 1);
        return hit.position;
    }
}

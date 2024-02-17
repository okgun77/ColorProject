using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateWander : IState
{
    private StateManager stateManager;
    private NavMeshAgent nav;
    private Animator anim;
    private Transform playerTr;


    private float checkTime = 2f;
    private float timer;

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
        if (timer > checkTime)
        {
            nav.SetDestination(GetRandomDestination());
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
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
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }
}

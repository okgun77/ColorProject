using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public IState curState;
    public EnemyAI enemyAI;
    public NavMeshAgent nav;
    public Animator anim;
    public Transform playerTr;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        curState = new StateWander(this, nav, anim, playerTr);
        curState.OnEnter();
    }

    private void Update()
    {
        curState.OnUpdate();
    }

    public void ChangeState(IState _newState)
    {
        curState.OnExit();
        curState = _newState;
        curState.OnEnter();
    }


}

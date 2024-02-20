using System;
using UnityEngine;
using UnityEngine.AI;

public class StateChase : IState
{
    private StateWander stateWander;

    private NavMeshAgent agent;
    private Transform playerTransform;
    private float chaseSpeed;
    private float chaseDistance;
    private Action<bool, bool> setAnimation;
    private NPCStateIndicator stateIndicator;
    private StateManager stateManager;

    private float transitionDelay = 2f;
    private float transitionTimer = 0f;

    public StateChase(NavMeshAgent _agent, Transform _playerTransform, float _chaseDistance, float _chaseSpeed, Action<bool, bool> _setAnimation, NPCStateIndicator _stateIndicator, StateManager _stateManager)
    {
        this.agent = _agent;
        this.playerTransform = _playerTransform;
        this.chaseDistance = _chaseDistance;
        this.chaseSpeed = _chaseSpeed;
        this.setAnimation = _setAnimation;
        this.stateIndicator = _stateIndicator;
        this.stateManager = _stateManager;
    }

    public void OnEnter()
    {
        agent.speed = chaseSpeed;
        transitionTimer = 0f;
        stateIndicator.UpdateStateText("Chase");

    }

    public void OnUpdate()
    {
        Chase();

        if (Vector3.Distance(agent.transform.position, playerTransform.position) > chaseDistance)
        {
            // 추적 범위 밖으로 벗어나면 타이머 시작 또는 계속
            transitionTimer += Time.deltaTime;

            // 충분한 시간이 지나면 방황 상태로 전환
            if (transitionTimer >= transitionDelay)
            {
                stateManager.ChangeState(stateManager.GetStateWander());
                transitionTimer = 0f; // 다음 번을 위해 타이머 초기화
            }
        }
        else
        {
            // 플레이어가 다시 추적 범위 내에 있으면 타이머 초기화
            transitionTimer = 0f;
        }
    }

    public void OnExit()
    {
        // Chase 상태 종료 시 필요한 로직
    }

    private void Chase()
    {
        agent.SetDestination(playerTransform.position);
        setAnimation(true, false);
    }
}

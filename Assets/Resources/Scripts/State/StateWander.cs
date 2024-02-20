using System;
using UnityEngine;
using UnityEngine.AI;

public class StateWander : IState
{
    private NavMeshAgent agent;
    private float wanderSpeed;
    private Action<bool, bool> setAnimation;
    private NPCStateIndicator stateIndicator;
    private StateManager stateManager;

    private float checkTime = 2f;   // 목표지점 갱신 주기
    private float timer = 0f;
    private float wanderRadius;   // 방황 범위

    


    public StateWander(NavMeshAgent _agent, float _wanderSpeed, Action<bool, bool> _setAnimation, float _wanderRadius, NPCStateIndicator _stateIndicator, StateManager _stateManager)
    {
        this.agent = _agent;
        this.wanderSpeed = _wanderSpeed;
        this.setAnimation = _setAnimation;
        this.wanderRadius = _wanderRadius;
        this.stateIndicator = _stateIndicator;
        this.stateManager = _stateManager;
    }

    public void OnEnter()
    {
        agent.speed = wanderSpeed;
        SetNewDestination();
        stateIndicator.UpdateStateText("Wander");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= checkTime || agent.remainingDistance < 0.5f)
        {
            SetNewDestination();
            timer = 0f;
        }

        // 여기에 플레이어와의 거리 체크 및 상태 전환 로직 추가
        CheckTransitions();
    }

    public void OnExit()
    {
        // Wander 상태 종료 시 필요한 로직
    }

    private void SetNewDestination()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius + agent.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, -1);
        agent.SetDestination(hit.position);
    }

    private void CheckTransitions()
    {
        // StateManager를 사용하여 현재 상황에 맞는 상태 전환 로직 구현
        if (stateManager == null) return;
        stateManager.CheckTransitions();
    }

    // 무작위 위치를 생성하는 함수
    private Vector3 RandomNavSphere(Vector3 _origin, float _dist, int _layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * _dist;
        randDirection += _origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, _dist, _layermask);

        Debug.Log($"Random Direction: {randDirection}, Final Position: {navHit.position}");

        return navHit.position;
    }

}

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
    

    public StateChase(NavMeshAgent _agent, Transform _playerTransform, float _chaseDistance, float _chaseSpeed, Action<bool, bool> _setAnimation)
    {
        this.agent = _agent;
        this.playerTransform = _playerTransform;
        this.chaseDistance = _chaseDistance;
        this.chaseSpeed = _chaseSpeed;
        this.setAnimation = _setAnimation;
    }

    public void OnEnter()
    {
        // agent.speed = chaseSpeed;
        Debug.Log($"Setting speed to: {chaseSpeed} (Chase)");
        agent.speed = chaseSpeed; // 또는 chaseSpeed
        
    }

    public void OnUpdate()
    {
        Chase();
        // agent.SetDestination(playerTransform.position);
        // 필요하다면 추적 중 애니메이션 변경
        // setAnimation?.Invoke(false, true); // 예시로 뛰는 애니메이션 활성화
        Debug.Log("State Chase");
    }

    public void OnExit()
    {
        // Chase 상태 종료 시 필요한 로직
    }

    private void Chase2()
    {
        agent.SetDestination(playerTransform.position);
        setAnimation(true, false);
    }

    private void Chase()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (distanceToPlayer <= chaseDistance)
        {
            agent.SetDestination(playerTransform.position); // 플레이어 추적
            setAnimation(true, false); // 걷는 애니메이션 활성화
        }
        else
        {
            // 추적 거리 밖이면 다른 행동을 취할 수 있습니다.
        }
    }
}

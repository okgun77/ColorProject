using System;
using UnityEngine;
using UnityEngine.AI;

public class StateFlee : IState
{
    private StateWander stateWander;
    
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float runDistance;
    private float fleeSpeed;
    private Action<bool, bool> setAnimation; // 예시로 애니메이션 설정을 위한 Action 추가


    public StateFlee(NavMeshAgent _agent, Transform _playerTransform, float _runDistance, float _fleeSpeed, Action<bool, bool> _setAnimation)
    {
        this.agent = _agent;
        this.playerTransform = _playerTransform;
        this.runDistance = _runDistance;
        this.fleeSpeed = _fleeSpeed;
        this.setAnimation = _setAnimation;
    }

    public void OnEnter()
    {
        Debug.Log($"Setting speed to: {fleeSpeed} (Flee)");
        agent.speed = fleeSpeed; // 또는 chaseSpeed
        
    }

    public void OnUpdate()
    {
        Flee();
        Debug.Log("State Flee");
    }

    public void OnExit()
    {
        
    }

    private void Flee()
    {
        float distanceToPlayer = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (distanceToPlayer < runDistance)
        {
            Vector3 fleeDirection = agent.transform.position - playerTransform.position;
            Vector3 newPos = agent.transform.position + fleeDirection.normalized * runDistance;
            agent.speed = fleeSpeed;
            agent.SetDestination(newPos);
            setAnimation(true, false); // 걷는 애니메이션 활성화
        }
        else
        {
            // 도망 거리 밖이면 다른 행동을 취할 수 있습니다.
        }
    }

}

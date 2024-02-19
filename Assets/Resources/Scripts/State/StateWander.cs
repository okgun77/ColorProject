using System;
using UnityEngine;
using UnityEngine.AI;

public class StateWander : IState
{
    private NavMeshAgent agent;
    private float wanderSpeed;
    private Action<bool, bool> setAnimation;

    private float checkTime = 2f;   // 목표지점 갱신 주기
    private float timer = 0f;
    private float wanderRadius = 10f;   // 방황 범위

    private NPCStateDisplay stateDisplay;


    public StateWander(NavMeshAgent _agent, float _wanderSpeed, Action<bool, bool> _setAnimation, float _wanderRadius, NPCStateDisplay _stateDisplay)
    {
        this.agent = _agent;
        this.wanderSpeed = _wanderSpeed;
        this.setAnimation = _setAnimation;
        this.wanderRadius = _wanderRadius;
        this.stateDisplay = _stateDisplay;
    }

    public void OnEnter()
    {
        agent.speed = wanderSpeed;
        SetNewDestination();
        // 상태 표시를 업데이트합니다.
        if (stateDisplay != null)
        {
            stateDisplay.UpdateStateDisplay("Wander");
        }
        
    }

    public void OnUpdate()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Vector3 newPos = RandomNavSphere(agent.transform.position, 10f, -1); // 10은 무작위 이동 반경
            agent.SetDestination(newPos);
        }
        Debug.Log("State Wander");
    }

    //public void OnUpdate()
    //{
    //    timer += Time.deltaTime;
    //    if (timer > checkTime)
    //    {
    //        SetNewDestination();
    //        timer = 0f;
    //    }
    //}

    public void OnExit()
    {
        // Wander 상태 종료 시 필요한 로직
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

    private void SetNewDestination()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
        randomDirection += agent.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1);
        agent.SetDestination(navHit.position);
    }
}

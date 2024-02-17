using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateFlee : IState
{
    private StateManager stateManager;
    private NavMeshAgent nav;
    private Transform playerTr;
    private Animator anim;

    // Constructor
    public StateFlee(StateManager _stateManager, NavMeshAgent _nav, Animator _anim, Transform _playerTr)
    {
        stateManager = _stateManager;
        nav = _nav;
        anim = _anim;
        playerTr = _playerTr;
    }

    public void OnEnter()
    {
        // 도망 상태가 시작될 때 도망 치는 방향으로 목적지를 설정
        Vector3 fleeDirection = (stateManager.transform.position - playerTr.position).normalized * 10; // 10은 도망치는 거리를 나타냅니다.
        Vector3 fleePosition = stateManager.transform.position + fleeDirection;
        nav.SetDestination(fleePosition);
        nav.speed = stateManager.fleeSpeed; // 도망 상태의 속도 설정
        anim.SetBool("IsMoving", true); // 도망 상태의 애니메이션 활성화
    }

    public void OnUpdate()
    {
        // 도망 중 플레이어와의 거리를 체크하여 상태를 변경할 수 있음
        if (!IsPlayerInRange())
        {
            stateManager.ChangeState(new StateWander(stateManager, nav, anim, playerTr));
        }
        else
        {
            // 플레이어로부터 계속 도망치기 위해 방향을 지속적으로 갱신
            Vector3 fleeDirection = (stateManager.transform.position - playerTr.position).normalized * 10;
            Vector3 newFleePosition = stateManager.transform.position + fleeDirection;
            nav.SetDestination(newFleePosition);
        }
    }

    public void OnExit()
    {
        // 도망 상태 종료 시의 로직, 예를 들어 애니메이션 상태 변경
        anim.SetBool("IsMoving", false);
    }

    private bool IsPlayerInRange()
    {
        // 플레이어와의 현재 거리를 계산하여 도망 거리 내에 있는지 확인
        float distanceToPlayer = Vector3.Distance(nav.transform.position, playerTr.position);
        return distanceToPlayer <= stateManager.runDistance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float runDistance = 4f; // Player와의 도망 거리
    [SerializeField] private float chaseDistance = 8f; // Player를 추적하기 시작하는 거리
    private float wanderTimer;
    private Transform player; // 플레이어의 Transform을 저장할 변수
    private bool isChasing = false; // 플레이어 추적 중인지 여부

    private ColorMixState colorMixState;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(1f, 4f);

        // 이동 속도 초기화
        navMeshAgent.speed = moveSpeed;

        // 플레이어를 찾아서 Transform을 저장
        player = GameObject.FindGameObjectWithTag("Player").transform;
        

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            // 일정 거리 안에 플레이어가 있다면 추적 상태로 전환
            isChasing = true;
        }
        
        if (!IsPlayerInRange())
        {
            if (isChasing)
            {
                // 추적 중인데 플레이어가 도망가면 추적 종료
                isChasing = false;
            }
            
            Wander();
        }
        else
        {
            if (isChasing)
            {
                if (colorMixState.MixingState == EColorMixingState.MIX_ING)
                {
                    // 플레이어가 색상 조합 중이면 도망 상태로 전환
                    Flee();
                }
                else
                {
                    // 플레이어가 다른 상태(예: 색상 조합 완료)이면 추적 상태로 전환
                    Chase();
                }
            }
            else
            {
                // 플레이어가 일정 거리 안에 있지만 추적 중이 아니면 도망
                Flee();
            }
        }
        
        // curState.UpdateState();
    }

    // 떠돌아다님
    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            Vector3 newPos = RandomNavSphere(transform.position, runDistance, -1);
            navMeshAgent.SetDestination(newPos);
            wanderTimer = Random.Range(1f, 2f);

            // 이동 속도 설정
            navMeshAgent.speed = moveSpeed;
        }
    }

    // 플레이어 추적
    void Chase()
    {
        // 플레이어를 추적하는 로직 추가
        navMeshAgent.SetDestination(player.position);

        // 이동 속도 설정
        navMeshAgent.speed = runSpeed;
    }

    void Flee()
    {
        Vector3 directionToPlayer = transform.position - player.position;
        Vector3 newPos = transform.position + directionToPlayer.normalized * runDistance;
        navMeshAgent.SetDestination(newPos);

        // 이동 속도 설정
        navMeshAgent.speed = moveSpeed;
    }

    bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= runDistance;
    }
    
    // 무작위로 위치를 생성하는 함수
    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}

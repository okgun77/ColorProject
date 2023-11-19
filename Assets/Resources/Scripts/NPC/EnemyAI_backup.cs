using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI_backup : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float runSpeed = 0.2f;
    [SerializeField] private float chaseSpeed = 0.3f;
    [SerializeField] private float runDistance = 3f; // Player와의 거리
    [SerializeField] private float chaseDistance = 3f; // Player를 쫓아가는 거리

    private NPCColor npcColor;
    private NavMeshAgent nav;
    private float wanderTimer;

    private Animator anim;
    
    
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        npcColor = GetComponent<NPCColor>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        wanderTimer = Random.Range(1f, 2f);
        // AnimRandomStart();
        
        // 애니메이션 시작 시점을 무작위로 설정
        float randomStartTime = Random.Range(0.0f, 1.0f); // 0과 1 사이의 무작위 값
        anim.Play("walk", 0, randomStartTime); // "Walk"는 재생할 애니메이션 상태의 이름
        SetAnimation(true, false); // 기본 상태로 걷는 애니메이션 활성화
    }

    private void Update()
    {
        NPCStatusChange();
        
        // 플레이어와의 거리에 따라 행동 결정
        if (IsPlayerInChaseRange())
        {
            Chase();
        }
        else if (IsPlayerInRange())
        {
            Flee();
        }
        else
        {
            Wander();
        }
    }

    private void AnimRandomStart()
    {
        // 애니메이션 시작 시점을 무작위로 설정
        float randomStartTime = Random.Range(0.0f, 1.0f); // 0과 1 사이의 무작위 값
        anim.Play("RUN", 0, randomStartTime); // "Walk"는 재생할 애니메이션 상태의 이름
        SetAnimation(true, false); // 기본 상태로 걷는 애니메이션 활성화
    }
    // 플레이어 컬러매치 상태에 따른 NPC 행동 변경
    private void NPCStatusChange()
    {
        EColorMatchStatus colorMatchStatus = GameManager.Instance.GetCurrentColorMatchStatus();

        switch (npcColor.Type)
        {
            case NPCType.NPC_COLOR:
                NPCColorStatusChange(colorMatchStatus);
                break;
            case NPCType.NPC_WATER:
                NPCWaterStatusChange(colorMatchStatus);
                break;
        }
    }
        
    
    private void NPCColorStatusChange(EColorMatchStatus status)
    {
        if (status == EColorMatchStatus.MIX_ING)
        {
            Flee();
        }
        else if (status == EColorMatchStatus.MIX_COMPLETE || status == EColorMatchStatus.MIX_FAIL)
        {
            Chase();
        }
    }

    private void NPCWaterStatusChange(EColorMatchStatus status)
    {
        if (status == EColorMatchStatus.MIX_FAIL)
        {
            Flee();
        }
        else
        {
            Chase();
        }
    }

    
    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            Vector3 newPos = RandomNavSphere(transform.position, runDistance, -1);
            nav.speed = moveSpeed;
            nav.SetDestination(newPos);
            wanderTimer = Random.Range(1f, 2f);
            SetAnimation(true, false); // 걷는 애니메이션 활성화
        }
    }

    void Flee()
    {
        Vector3 directionToPlayer = transform.position - GetPlayerPosition();
        Vector3 newPos = transform.position + directionToPlayer.normalized * runDistance;
        nav.speed = runSpeed;
        nav.SetDestination(newPos);
        // SetAnimation(false, true); // 뛰는 애니메이션 활성화
        SetAnimation(true, false); // 걷는 애니메이션 활성화
    }

    void Chase()
    {
        Vector3 directionToPlayer = GetPlayerPosition() - transform.position;
        Vector3 newPos = transform.position + directionToPlayer.normalized * chaseDistance;
        nav.speed = chaseSpeed;
        nav.SetDestination(newPos);
        SetAnimation(true, false); // 걷는 애니메이션 활성화
    }

    bool IsPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= runDistance;
        }
        return false;
    }

    bool IsPlayerInChaseRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= chaseDistance;
        }
        return false;
    }

    Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector3.zero;
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
    
    
    private void SetAnimation(bool isWalking, bool isRunning)
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
    }
}
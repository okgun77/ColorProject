using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
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
    }

    private void Update()
    {
        NPCStatusChange();
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
        }
    }

    void Flee()
    {
        Vector3 directionToPlayer = transform.position - GetPlayerPosition();
        Vector3 newPos = transform.position + directionToPlayer.normalized * runDistance;
        nav.speed = runSpeed;
        nav.SetDestination(newPos);
    }

    void Chase()
    {
        Vector3 directionToPlayer = GetPlayerPosition() - transform.position;
        Vector3 newPos = transform.position + directionToPlayer.normalized * chaseDistance;
        nav.speed = chaseSpeed;
        nav.SetDestination(newPos);
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
}
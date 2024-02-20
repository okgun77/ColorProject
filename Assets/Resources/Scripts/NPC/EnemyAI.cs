using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] private float wanderSpeed = 0.1f;  // 서성일때 속도
    [SerializeField] private float fleeSpeed = 0.2f;    // 도망갈때 속도
    [SerializeField] private float chaseSpeed = 0.3f;   // 추적할 때 속도
    [SerializeField] private float runDistance = 3f;    // 도망 거리
    [SerializeField] private float chaseDistance = 3f;  // 추적 거리
    [SerializeField] private float wanderRadius = 10;   // 배회 반경


    public float WanderSpeed => wanderSpeed;
    public float FleeSpeed => fleeSpeed;
    public float ChaseSpeed => chaseSpeed;
    public float RunDistance => runDistance;
    public float ChaseDistance => chaseDistance;
    public NPCColor NpcColor => npcColor;


    private NPCColor npcColor;
    private NavMeshAgent nav;
    private float wanderTimer;
    private Animator anim;
    private Transform playerTransform;

    public Transform PlayerTransform => playerTransform;

    private StateManager stateManager;
    private StateWander stateWander;
    private StateFlee stateFlee;
    private StateChase stateChase;
    private NPCStateIndicator stateIndicator;


    
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        npcColor = GetComponent<NPCColor>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        stateIndicator = GetComponentInChildren<NPCStateIndicator>();


        // 상태 객체 초기화
        stateWander = new StateWander(nav, wanderSpeed, SetAnimation, wanderRadius, stateIndicator, stateManager);
        stateFlee = new StateFlee(nav, playerTransform, runDistance, fleeSpeed, SetAnimation, stateIndicator, stateManager);
        stateChase = new StateChase(nav, playerTransform, chaseDistance, chaseSpeed, SetAnimation, stateIndicator, stateManager);

        // StateManager 초기화
        stateManager = new StateManager(this, stateWander, stateFlee, stateChase);
    }

    private void Start()
    {
        wanderTimer = Random.Range(1f, 2f);
        // AnimRandomStart();
        
        // 애니메이션 시작 시점을 무작위로 설정
        float randomStartTime = Random.Range(0.0f, 1.0f);
        anim.Play("walk", 0, randomStartTime); // "RUN" 재생 애니메이션 상태
        SetAnimation(true, false); // 기본 상태 걷는 애니메이션


        // 상태 인스턴스 생성
        // stateWander = new StateWander(nav, wanderSpeed, SetAnimation);
        // stateFlee = new StateFlee(nav, playerTransform, runDistance, fleeSpeed, SetAnimation);
        // stateChase = new StateChase(nav, playerTransform, chaseDistance, chaseSpeed, SetAnimation);

        // 초기 상태 설정
        stateManager.ChangeState(stateWander);
    }

    private void Update()
    {
        // NPCStatusChange();

        stateManager.UpdateState();
    }

    private void AnimRandomStart()
    {
        // 애니메이션 시작 시점을 무작위로 설정
        float randomStartTime = Random.Range(0.0f, 1.0f);
        anim.Play("walk", 0, randomStartTime); // "RUN" 재생 애니메이션 상태
        SetAnimation(true, false); // 기본 상태 걷는 애니메이션
    }


    public IState GetStateFlee()
    {
        return stateFlee;
    }

    public IState GetStateChase()
    {
        return stateChase;
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

    public bool IsPlayerInRangeCheck()
    {
        return IsPlayerInRange();
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

    public Vector3 GetPlayerPositionCheck()
    {
        return GetPlayerPosition();
    }

    private void SetAnimation(bool isWalking, bool isRunning)
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
    }
}
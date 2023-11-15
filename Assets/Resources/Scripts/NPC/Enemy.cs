using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected string EnemyName; // �의 �름
    [SerializeField] protected int hp; // �의 체력.

    [SerializeField] protected float walkSpeed; // 걷기 �피
    [SerializeField] protected float runSpeed; // �기 �피
    
    protected Vector3 destination; // 목적지.

    // �태변
    protected bool isAction; // �동중인지 �닌지 �별.
    protected bool isWalking; // 걷는지 걷는지 �별.
    protected bool isRunning; // �는지 �별.
    protected bool isChasing; // 추격중인지 �별.

    [SerializeField] protected float walkTime; // 걷기 �간
    [SerializeField] protected float waitTime; // �긜간.
    [SerializeField] protected float runTime; // �기 �간.
    protected float currentTime;


    // �요컴포�트
    // [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    protected NavMeshAgent nav;
    protected FieldOfViewAngle theViewAngle;

    [SerializeField]
    protected float ChaseTime; // �추격 �간
    protected float CurrentChaseTime; // 계산
    [SerializeField]
    protected float ChaseDelayTime; // 추격 �레

    // Use this for initialization
    void Start()
    {
        theViewAngle = GetComponent<FieldOfViewAngle>();
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {        
            Move(); 
            ElapseTime();       
    }


    protected void Move()
    {
        if (isWalking || isRunning)
           nav.SetDestination(transform.position + destination * 5f);
    }


    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isChasing)
                ReSet();
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false; isRunning = false; isAction = true;
        nav.speed = walkSpeed;
        nav.ResetPath();
        // anim.SetBool("Walking", isWalking); anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
    }

    protected void TryWalk()
    {
        isWalking = true;
        // anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
        Debug.Log("걷기");
    }

    public void Run(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x - _targetPos.x,1f, transform.position.z - _targetPos.z).normalized;
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;
        // anim.SetBool("Running", isRunning);
    }

    public void Chase(Vector3 _targetPos)
    {
        isChasing = true;
        destination = _targetPos;
        nav.speed = runSpeed;
        // anim.SetBool("Running", isRunning);
        nav.SetDestination(destination);
    }

}

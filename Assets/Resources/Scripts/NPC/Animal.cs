using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    [SerializeField] protected string animalName; // 동물의 이름
    [SerializeField] protected int hp; // 동물의 체력.

    [SerializeField] protected float walkSpeed; // 걷기 스피드.
    [SerializeField] protected float runSpeed; // 뛰기 스피드.
    
    protected Vector3 destination; // 목적지.

    // 상태변수
    protected bool isAction; // 행동중인지 아닌지 판별.
    protected bool isWalking; // 걷는지 안 걷는지 판별.
    protected bool isRunning; // 뛰는지 판별.
    protected bool isChasing; // 추격중인지 판별.

    [SerializeField] protected float walkTime; // 걷기 시간
    [SerializeField] protected float waitTime; // 대기 시간.
    [SerializeField] protected float runTime; // 뛰기 시간.
    protected float currentTime;


    // 필요한 컴포넌트
    // [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    protected NavMeshAgent nav;
    protected FieldOfViewAngle theViewAngle;

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
    
}

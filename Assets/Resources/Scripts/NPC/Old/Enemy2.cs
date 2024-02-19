using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float runSpeed = 0.2f;
    [SerializeField] private float runDistance = 5f; // Player�거리
    private NavMeshAgent nav;
    private float wanderTimer;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(1f, 2f);
    }
    void Update()
    {
        if (!IsPlayerInRange())
        {
            Wander();
        }
        else
        {
            Flee();
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
    Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector3.zero;
    }

    // 무작�로 �치륝성�는 �수
    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
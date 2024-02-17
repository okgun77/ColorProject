using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float wanderSpeed = 0.1f;
    [SerializeField] private float fleeSpeed = 0.2f;
    [SerializeField] private float chaseSpeed = 0.3f;
    [SerializeField] private float detectionRange = 10f;

    private StateManager stateManager;
    private NavMeshAgent navAgent;
    private Transform playerTransform;

    private void Awake()
    {
        stateManager = GetComponent<StateManager>();
        navAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        CheckForStateChange();
    }

    void CheckForStateChange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < detectionRange)
        {
            stateManager.ChangeState(new StateChase(stateManager, navAgent, stateManager.animator, playerTransform));
        }
        else
        {
            stateManager.ChangeState(new StateWander(stateManager, navAgent, stateManager.animator, playerTransform));
        }
    }
}

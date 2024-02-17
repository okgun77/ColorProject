using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public IState currentState;
    public NavMeshAgent navAgent;
    public Animator animator;
    public Transform playerTransform;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = new StateWander(this, navAgent, animator, playerTransform);
        currentState.OnEnter();
    }

    void Update()
    {
        currentState.OnUpdate();
    }

    public void ChangeState(IState _newState)
    {
        currentState.OnExit();
        currentState = _newState;
        currentState.OnEnter();
    }
}

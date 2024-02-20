using System;
using UnityEngine;
using UnityEngine.AI;

public class StateFlee : IState
{
    private StateWander stateWander;
    
    private NavMeshAgent agent;
    private Transform playerTransform;
    private float runDistance;
    private float fleeSpeed;
    private Action<bool, bool> setAnimation; // 예시로 애니메이션 설정을 위한 Action 추가
    private NPCStateIndicator stateIndicator;
    private StateManager stateManager;

    private float transitionDelay = 2f;
    private float transitionTimer = 0.0f;


    public StateFlee(NavMeshAgent _agent, Transform _playerTransform, float _runDistance, float _fleeSpeed, Action<bool, bool> _setAnimation, NPCStateIndicator _stateIndicator, StateManager _stateManager)
    {
        this.agent = _agent;
        this.playerTransform = _playerTransform;
        this.runDistance = _runDistance;
        this.fleeSpeed = _fleeSpeed;
        this.setAnimation = _setAnimation;
        this.stateIndicator = _stateIndicator;
        this.stateManager = _stateManager;
    }

    public void OnEnter()
    {
        agent.speed = fleeSpeed; // 또는 chaseSpeed
        transitionTimer = 0.0f;
        stateIndicator.UpdateStateText("Flee");
    }

    public void OnUpdate()
    {
        Flee();

        if (Vector3.Distance(agent.transform.position, playerTransform.position) > runDistance)
        {
            // Start or continue the transition timer
            transitionTimer += Time.deltaTime;

            // If enough time has passed since starting to flee, transition to wander
            if (transitionTimer >= transitionDelay)
            {
                stateManager.ChangeState(stateManager.GetStateWander());
                transitionTimer = 0f; // Reset timer for next time
            }
        }
        else
        {
            // Reset the timer if player is again within flee distance
            transitionTimer = 0f;
        }
    }

    public void OnExit()
    {
        
    }

    private void Flee()
    {
        Vector3 directionAwayFromPlayer = agent.transform.position - playerTransform.position;
        Vector3 fleeDirection = directionAwayFromPlayer.normalized * runDistance;
        Vector3 newPos = agent.transform.position + fleeDirection;
        agent.SetDestination(newPos);
        setAnimation(true, false); // Assume this sets the correct animation for fleeing
    }

}

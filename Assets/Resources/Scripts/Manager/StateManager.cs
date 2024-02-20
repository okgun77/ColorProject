using UnityEngine;
public class StateManager
{
    private IState currentState;
    private IState stateWander;
    private IState stateFlee;
    private IState stateChase;


    private EnemyAI enemyAI;
    


    // StateManager 생성자
    public StateManager(EnemyAI _enemyAI, IState _stateWander, IState _stateFlee, IState _stateChase)
    {
        this.enemyAI = _enemyAI;
        this.stateWander = _stateWander;
        this.stateFlee = _stateFlee;
        this.stateChase = _stateChase;

        currentState = stateWander;

    }

    public void ChangeState(IState _newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = _newState;
        currentState.OnEnter();
    }

    public void UpdateState()
    {
        currentState?.OnUpdate();
        CheckTransitions();
    }

    public IState GetStateWander()
    {
        return stateWander;
    }

    public void CheckTransitions()
    {
        if (enemyAI == null) return;

        float distanceToPlayer = Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTransform.position);
        var colorMatchStatus = GameManager.Instance.GetCurrentColorMatchStatus();

        // 조건에 따라 상태 전환 로직 구현
        if (currentState == stateWander)
        {
            if (ShouldFlee(colorMatchStatus, distanceToPlayer))
            {
                ChangeState(stateFlee);
            }
            else if (ShouldChase(colorMatchStatus, distanceToPlayer))
            {
                ChangeState(stateChase);
            }
        }
        else if (currentState == stateFlee && distanceToPlayer > enemyAI.RunDistance)
        {
            ChangeState(stateWander);
        }
        else if (currentState == stateChase && distanceToPlayer > enemyAI.ChaseDistance)
        {
            ChangeState(stateWander);
        }
    }

    private bool ShouldFlee(EColorMatchStatus colorMatchStatus, float distanceToPlayer)
    {
        return (colorMatchStatus == EColorMatchStatus.MIX_ING && distanceToPlayer <= enemyAI.RunDistance) ||
               (enemyAI.NpcColor.Type == NPCType.NPC_WATER && colorMatchStatus == EColorMatchStatus.MIX_FAIL && distanceToPlayer <= enemyAI.RunDistance);
    }

    private bool ShouldChase(EColorMatchStatus colorMatchStatus, float distanceToPlayer)
    {
        return ((colorMatchStatus == EColorMatchStatus.MIX_COMPLETE || colorMatchStatus == EColorMatchStatus.MIX_FAIL) && distanceToPlayer <= enemyAI.ChaseDistance) ||
               (enemyAI.NpcColor.Type == NPCType.NPC_COLOR && (colorMatchStatus == EColorMatchStatus.MIX_ING || colorMatchStatus == EColorMatchStatus.MIX_COMPLETE) && distanceToPlayer <= enemyAI.ChaseDistance);
    }
}
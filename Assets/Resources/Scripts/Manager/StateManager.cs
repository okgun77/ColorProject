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

        // NPCColor의 경우
        // Player 색상 혼합중 : Flee
        // Player 색상 혼합실패, 혼합성공 : Chase
        // 향후 개선사항 : 현재는 색상 상관없이 일괄적으로 도망가거나 쫓아가는데,
        //               목표색상에 필요한 색상과 필요없는 색상을 구분해서 행동패턴 개선
        if (enemyAI.NpcColor.Type == NPCType.NPC_COLOR)
        {
            if (colorMatchStatus == EColorMatchStatus.MIX_ING && distanceToPlayer <= enemyAI.RunDistance)
            {
                ChangeState(stateFlee);
            }
            else if ((colorMatchStatus == EColorMatchStatus.MIX_COMPLETE || colorMatchStatus == EColorMatchStatus.MIX_FAIL)
                      && distanceToPlayer <= enemyAI.ChaseDistance)
            {
                ChangeState(stateChase);
            }
            else { ChangeState(stateWander); }
        }
        // NPCWater의 경우
        // Player 색상 혼합중, 혼합성공 : Chase
        // Player 색상 혼합실패 : Flee
        // 추적거리 밖 : Wander
        else if (enemyAI.NpcColor.Type == NPCType.NPC_WATER)
        {
            if (colorMatchStatus == EColorMatchStatus.MIX_FAIL && distanceToPlayer <= enemyAI.RunDistance)
            {
                ChangeState(stateFlee);
            }
            else if ((colorMatchStatus == EColorMatchStatus.MIX_ING || colorMatchStatus == EColorMatchStatus.MIX_COMPLETE)
                      && distanceToPlayer <= enemyAI.ChaseDistance)
            {
                ChangeState(stateChase);
            }
            else { ChangeState(stateWander);}
        }
    }

}
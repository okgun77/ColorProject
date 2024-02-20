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

    private void CheckTransitions()
    {
        if (enemyAI == null) return;

        float distanceToPlayer = Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTransform.position);
        var colorMatchStatus = GameManager.Instance.GetCurrentColorMatchStatus();

        switch (enemyAI.NpcColor.Type)
        {
            case NPCType.NPC_COLOR:
                HandleNpcColorTransitions(colorMatchStatus, distanceToPlayer);
                break;
            case NPCType.NPC_WATER:
                HandleNpcWaterTransitions(colorMatchStatus, distanceToPlayer);
                break;
        }
    }

    private void HandleNpcColorTransitions(EColorMatchStatus _colorMatchStatus, float _distanceToPlayer)
    {
        if (_colorMatchStatus == EColorMatchStatus.MIX_ING && _distanceToPlayer <= enemyAI.RunDistance)
        {
            ChangeState(stateFlee);
        }
        else if ((_colorMatchStatus == EColorMatchStatus.MIX_COMPLETE || _colorMatchStatus == EColorMatchStatus.MIX_FAIL) && _distanceToPlayer <= enemyAI.ChaseDistance)
        {
            ChangeState(stateChase);
        }
        else
        {
            ChangeState(stateWander);
        }
    }

    private void HandleNpcWaterTransitions(EColorMatchStatus _colorMatchStatus, float _distanceToPlayer)
    {
        if (_colorMatchStatus == EColorMatchStatus.MIX_FAIL && _distanceToPlayer <= enemyAI.RunDistance)
        {
            ChangeState(stateFlee);
        }
        else if ((_colorMatchStatus == EColorMatchStatus.MIX_ING || _colorMatchStatus == EColorMatchStatus.MIX_COMPLETE) && _distanceToPlayer <= enemyAI.ChaseDistance)
        {
            ChangeState(stateChase);
        }
        else
        {
            ChangeState(stateWander);
        }
    }


}

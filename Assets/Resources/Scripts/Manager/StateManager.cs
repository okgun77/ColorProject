public class StateManager
{
    private IState currentState;
    private EnemyAI enemyAI;


    // StateManager 생성자
    public StateManager(EnemyAI _enemyAI, IState _stateFlee, IState _stateChase)
    {
        this.enemyAI = _enemyAI;
    }

    public void ChangeState(IState _newState)
    {
        currentState?.OnExit();
        currentState = _newState;
        currentState.OnEnter();
    }

    public void UpdateState()
    {
        currentState?.OnUpdate();

        // 예제: 상태 전환 로직을 여기에 구현
        StateCheck();
    }

    private void StateCheck()
    {
        if (enemyAI == null) return;

        // 여기에서 EnemyAI의 상태를 확인하여 상태 전환을 결정합니다.
        // 예: 플레이어와의 거리에 따라 상태 변경
        var colorMatchStatus = GameManager.Instance.GetCurrentColorMatchStatus();
        switch (enemyAI.NpcColor.Type)
        {
            case NPCType.NPC_COLOR:
                if (colorMatchStatus == EColorMatchStatus.MIX_ING)
                {
                    ChangeState(enemyAI.GetStateFlee()); // Flee 상태로 전환
                }
                else if (colorMatchStatus == EColorMatchStatus.MIX_COMPLETE || colorMatchStatus == EColorMatchStatus.MIX_FAIL)
                {
                    ChangeState(enemyAI.GetStateChase()); // Chase 상태로 전환
                }
                break;
            case NPCType.NPC_WATER:
                if (colorMatchStatus == EColorMatchStatus.MIX_FAIL)
                {
                    ChangeState(enemyAI.GetStateFlee()); // Flee 상태로 전환
                }
                else
                {
                    ChangeState(enemyAI.GetStateChase()); // Chase 상태로 전환
                }
                break;
        }
    }


}

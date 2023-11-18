using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected EnemyAI enemyAI;

    public void Init(EnemyAI enemyAI)
    {
        this.enemyAI = enemyAI;
    }
    
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

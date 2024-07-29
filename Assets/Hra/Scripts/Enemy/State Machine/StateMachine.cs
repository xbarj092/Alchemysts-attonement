using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public EnemyState CurrentEnemyState {  get; set; }

    public void Initialize(EnemyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    private void Update()
    {
        if (CurrentEnemyState == null)
        {
            return;
        }

        EnemyState nextState = CurrentEnemyState?.ExecuteState();
        if (nextState != null && nextState != CurrentEnemyState)
        {
            ChangeState(nextState);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}

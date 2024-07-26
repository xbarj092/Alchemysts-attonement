using System;
using UnityEngine;

public class EnemyStateHit : EnemyState
{
    public EnemyStateHit(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTrigger trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }

    public override void EnterState()
    {
        Debug.Log($"Entered Hit state!");
        base.EnterState();
    }

    public override EnemyState ExecuteState()
    {
        if (IsHitCompleted())
        {
            return _enemy.ChasingState;
        }

        return base.ExecuteState();
    }

    private bool IsHitCompleted()
    {
        // implement hit func
        throw new NotImplementedException();
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Hit state!");
        base.ExitState();
    }
}

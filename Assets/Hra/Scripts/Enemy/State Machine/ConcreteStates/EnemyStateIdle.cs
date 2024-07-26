using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    public EnemyStateIdle(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTrigger trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }

    public override void EnterState()
    {
        Debug.Log($"Entered Idle state!");
        base.EnterState();
    }

    public override EnemyState ExecuteState()
    {
        if (_enemy.IsAggroed)
        {
            return _enemy.ChasingState;
        }

        if (_enemy.IsWithingAttackRange)
        {
            return _enemy.AttackState;
        }

        // do idle stuff

        return base.ExecuteState();
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Idle state!");
        base.ExitState();
    }
}

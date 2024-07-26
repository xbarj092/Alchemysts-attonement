using System;

public class EnemyStateAttack : EnemyState
{
    public EnemyStateAttack(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTrigger trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override EnemyState ExecuteState()
    {
        if (IsAttackCompleted())
        {
            return _enemy.ChasingState;
        }

        return base.ExecuteState();
    }

    private bool IsAttackCompleted()
    {
        // implement attack func
        throw new NotImplementedException();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

using UnityEngine;

public class EnemyStateChase : EnemyState
{
    public EnemyStateChase(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTrigger trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log($"Entered Chase state!");
    }

    public override EnemyState ExecuteState()
    {
        if (!_enemy.IsAggroed)
        {
            return _enemy.IdleState;
        }

        if (_enemy.IsWithingAttackRange)
        {
            return _enemy.AttackState;
        }

        // move it towards player
        _enemy.MoveEnemy(Vector2.zero);

        return base.ExecuteState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log($"Exited Chase state!");
    }
}

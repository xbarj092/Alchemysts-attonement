using UnityEngine;

public class EnemyStateChase : EnemyState
{
    public EnemyStateChase(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _enemy.Animator.PlayAnimation(EnemyAnimationTrigger.EnemyChase);

        Debug.Log($"Entered Chase state!");
    }

    public override IState ExecuteState()
    {
        if (!_enemy.IsAggroed)
        {
            return _enemy.IdleState;
        }

        if (_enemy.IsWithinAttackRange)
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

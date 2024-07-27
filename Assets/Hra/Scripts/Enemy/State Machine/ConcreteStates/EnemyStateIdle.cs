using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    private float _timeElapsed;

    private const float WAIT_TIME = 1f;

    public EnemyStateIdle(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        _timeElapsed = 0;
        _enemy.Animator.PlayAnimation(AnimationTrigger.EnemyIdle);
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

        if (FinishedWaiting())
        {
            return _enemy.RoamingState;
        }

        // do idle stuff

        return base.ExecuteState();
    }

    private bool FinishedWaiting()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= WAIT_TIME)
        {
            return true;
        }

        return false;
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Idle state!");
        base.ExitState();
    }
}

using UnityEngine;

public class EnemyStateHit : EnemyState
{
    private float _timeElapsed;

    private const float HIT_TIME = 1f;

    public EnemyStateHit(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        _timeElapsed = 0;
        _enemy.Animator.PlayAnimation(AnimationTrigger.EnemyHit);
        Debug.Log($"Entered Hit state!");
        base.EnterState();
    }

    public override EnemyState ExecuteState()
    {
        if (IsHitCompleted())
        {
            _enemy.IsHit = false;
            return _enemy.ChasingState;
        }

        return base.ExecuteState();
    }

    private bool IsHitCompleted()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= HIT_TIME)
        {
            return true;
        }

        return false;
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Hit state!");
        base.ExitState();
    }
}

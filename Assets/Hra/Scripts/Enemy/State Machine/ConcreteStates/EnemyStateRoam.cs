using UnityEngine;

public class EnemyStateRoam : EnemyState
{
    private Vector2 _targetPos;
    private Vector2 _direction;

    public EnemyStateRoam(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTrigger trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }

    public override void EnterState()
    {
        Debug.Log($"Entered Roam state!");
        base.EnterState();

        _targetPos = GetRandomPosition();
    }

    public override EnemyState ExecuteState()
    {
        if (_enemy.IsAggroed)
        {
            return _enemy.ChasingState;
        }

        if ((Vector2)_enemy.transform.position == _targetPos)
        {
            return _enemy.IdleState;
        }

        MoveEnemy();

        return base.ExecuteState();
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Roam state!");
        base.ExitState();
    }

    public void MoveEnemy()
    {
        if (_enemy.IsAggroed)
        {
            _enemy.StateMachine.ChangeState(_enemy.ChasingState);
        }

        _direction = (_targetPos - (Vector2)_enemy.transform.position).normalized;

        _enemy.MoveEnemy(_direction * _enemy.MovementSpeed);

        if (((Vector2)_enemy.transform.position - _targetPos).sqrMagnitude < 0.01)
        {
            _targetPos = GetRandomPosition();
        }
    }

    private Vector2 GetRandomPosition()
    {
        return (Vector2)_enemy.transform.position + (Random.insideUnitCircle * _enemy.MovementRange);
    }
}

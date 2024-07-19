using System;
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
        base.EnterState();

        _targetPos = GetRandomPosition();
    }


    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _direction = (_targetPos - (Vector2)enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * enemy.RandomMovementSpeed);

        if (((Vector2)enemy.transform.position - _targetPos).sqrMagnitude < 0.01)
        {
            _targetPos = GetRandomPosition();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector2 GetRandomPosition()
    {
        return (Vector2)enemy.transform.position + (UnityEngine.Random.insideUnitCircle * enemy.RandomMovementRange);
    }
}

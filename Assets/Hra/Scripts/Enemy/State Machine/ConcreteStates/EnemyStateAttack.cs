using System;
using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    private float _timeElapsed;

    private const float ATTACK_TIME = 1f;

    public EnemyStateAttack(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        _timeElapsed = 0;
        _enemy.Animator.PlayAnimation(AnimationTrigger.EnemyAttack);
        SpawnProjectile();
        Debug.Log($"Entered Attack state!");
        base.EnterState();
    }

    private void SpawnProjectile()
    {
        _enemy.UseWeapon();
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
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= ATTACK_TIME)
        {
            return true;
        }

        return false;
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Attack state!");
        base.ExitState();
    }
}

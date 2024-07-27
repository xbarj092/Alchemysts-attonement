using UnityEngine;

public class EnemyStateDeath : EnemyState
{
    public EnemyStateDeath(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        Debug.Log($"Entered Death state!");
        _enemy.Animator.PlayAnimation(AnimationTrigger.EnemyDeath);
        base.EnterState();
    }

    public override EnemyState ExecuteState()
    {
        return base.ExecuteState();
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Death state!");
        base.ExitState();
    }
}

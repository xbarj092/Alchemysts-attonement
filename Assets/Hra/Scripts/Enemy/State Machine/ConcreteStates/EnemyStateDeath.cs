using UnityEngine;

public class EnemyStateDeath : EnemyState
{
    public EnemyStateDeath(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        Debug.Log($"Entered Death state!");
        _enemy.Animator.PlayAnimation(EnemyAnimationTrigger.EnemyDeath);
        base.EnterState();
    }

    public override IState ExecuteState()
    {
        return base.ExecuteState();
    }

    public override void ExitState()
    {
        Debug.Log($"Exited Death state!");
        base.ExitState();
    }
}

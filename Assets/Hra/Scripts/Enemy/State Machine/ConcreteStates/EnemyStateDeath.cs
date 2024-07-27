using UnityEngine;

public class EnemyStateDeath : EnemyState
{
    public EnemyStateDeath(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Enemy.AnimationTrigger trigger)
    {
        base.AnimationTriggerEvent(trigger);
    }

    public override void EnterState()
    {
        Debug.Log($"Entered Death state!");
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

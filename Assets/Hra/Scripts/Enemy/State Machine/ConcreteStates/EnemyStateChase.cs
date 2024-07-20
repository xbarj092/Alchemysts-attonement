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

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.MoveEnemy(Vector2.zero);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

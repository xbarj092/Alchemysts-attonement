using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerController player, StateMachine enemyStateMachine) : base(player, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerDeath);
    }

    public override PlayerState ExecuteState()
    {
        return base.ExecuteState();
    }
}

using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerController player, StateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.Move(Vector2.zero);
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerDeath);
    }

    public override IState ExecuteState()
    {
        return base.ExecuteState();
    }
}

using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerController player, StateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        // _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerMoveLeft);
    }

    public override IState ExecuteState()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (movement.x == 0 && movement.y == 0)
        {
            _player.Move(Vector2.zero);
            return _player.IdleState;
        }

        _player.Move(movement);

        return base.ExecuteState();
    }
}

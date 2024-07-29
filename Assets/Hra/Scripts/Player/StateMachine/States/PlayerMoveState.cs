using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerController player, StateMachine enemyStateMachine) : base(player, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        // _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerMoveLeft);
    }

    public override PlayerState ExecuteState()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (movement.x != 0 || movement.y != 0)
        {
            return _player.IdleState;
        }

        _player.Move();

        return base.ExecuteState();
    }
}

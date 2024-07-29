using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController player, StateMachine enemyStateMachine) : base(player, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerIdle);
    }

    public override PlayerState ExecuteState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return _player.AttackState;
        }

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (movement.x != 0 || movement.y != 0)
        {
            return _player.MoveState;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return _player.DashState;
        }

        return base.ExecuteState();
    }
}

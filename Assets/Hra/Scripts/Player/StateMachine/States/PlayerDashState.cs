using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float _timeElapsed;

    private const float DASH_TIME = 1f;

    public PlayerDashState(PlayerController player, StateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _timeElapsed = 0;
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerDash);
    }

    public override IState ExecuteState()
    {
        if (IsDashCompleted())
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (movement.x != 0 || movement.y != 0)
            {
                return _player.MoveState;
            }
            else
            {
                return _player.IdleState;
            }
        }

        return base.ExecuteState();
    }

    private bool IsDashCompleted()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= DASH_TIME)
        {
            return true;
        }

        return false;
    }
}

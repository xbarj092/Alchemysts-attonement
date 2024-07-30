using UnityEngine;

public class PlayerHitState : PlayerState
{
    private float _timeElapsed;

    private const float HIT_TIME = 1f;

    public PlayerHitState(PlayerController player, StateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerHit);
    }

    public override IState ExecuteState()
    {
        if (IsHitCompleted())
        {
            _player.IsHit = false;
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

    private bool IsHitCompleted()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= HIT_TIME)
        {
            return true;
        }

        return false;
    }
}

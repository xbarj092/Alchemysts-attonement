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
        _timeElapsed = 0;
        _player.Move(Vector2.zero);
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerHit, 1, false);
        _player.Animator.SetLayerWeight(1, 1);
    }

    public override IState ExecuteState()
    {
        _timeElapsed += Time.deltaTime;

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _player.Move(movement);

        if (IsHitCompleted())
        {
            _player.IsHit = false;
            _player.Animator.SetLayerWeight(1, 0);

            if (movement != Vector2.zero)
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

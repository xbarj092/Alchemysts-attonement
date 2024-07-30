using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float _timeElapsed;

    private const float ATTACK_TIME = 1f;

    public PlayerAttackState(PlayerController player, StateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        _player.Animator.PlayAnimation(PlayerAnimationTrigger.PlayerAttack);
    }

    public override IState ExecuteState()
    {
        if (IsAttackCompleted())
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

    private bool IsAttackCompleted()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= ATTACK_TIME)
        {
            return true;
        }

        return false;
    }
}

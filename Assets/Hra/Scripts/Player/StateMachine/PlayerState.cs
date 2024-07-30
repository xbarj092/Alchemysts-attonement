public class PlayerState : IState
{
    protected PlayerController _player;
    protected StateMachine _playerStateMachine;

    public PlayerState(PlayerController player, StateMachine playerStateMachine)
    {
        _player = player;
        _playerStateMachine = playerStateMachine;
    }

    public virtual IState ExecuteState()
    {
        if (_player.IsHit)
        {
            return _player.HitState;
        }

        if (_player.IsDead)
        {
            return _player.DeathState;
        }

        return null;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
}

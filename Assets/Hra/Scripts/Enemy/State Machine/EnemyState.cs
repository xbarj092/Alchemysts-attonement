public class EnemyState : IState
{
    protected Enemy _enemy;
    protected StateMachine _enemyStateMachine;

    public EnemyState(Enemy enemy, StateMachine enemyStateMachine)
    {
        _enemy = enemy;
        _enemyStateMachine = enemyStateMachine;
    }

    public virtual IState ExecuteState()
    {
        if (_enemy.IsDead)
        {
            return _enemy.DeathState;
        }

        if (_enemy.IsHit)
        {
            return _enemy.HitState;
        }

        return null;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
}

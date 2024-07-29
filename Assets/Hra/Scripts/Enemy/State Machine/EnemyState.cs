public class EnemyState : IState<EnemyState>
{
    protected Enemy _enemy;
    protected StateMachine _enemyStateMachine;

    public EnemyState(Enemy enemy, StateMachine enemyStateMachine)
    {
        _enemy = enemy;
        _enemyStateMachine = enemyStateMachine;
    }

    public virtual EnemyState ExecuteState()
    {
        if (_enemy.IsHit)
        {
            return _enemy.HitState;
        }

        if (_enemy.IsDead)
        {
            return _enemy.DeathState;
        }

        return null;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
}

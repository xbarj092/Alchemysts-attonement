public interface IState<T>
{
    void EnterState();
    T ExecuteState();
    void ExitState();
}

public interface IState
{
    void EnterState();
    IState ExecuteState();
    void ExitState();
}

using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState CurrentState {  get; set; }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }

    private void Update()
    {
        if (CurrentState == null)
        {
            return;
        }

        IState nextState = CurrentState.ExecuteState();
        if (nextState != null && !nextState.Equals(CurrentState))
        {
            ChangeState(nextState);
        }
    }

    public void ChangeState(IState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}

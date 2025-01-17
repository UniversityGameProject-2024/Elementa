using UnityEngine;

public class StateMachine
{
    public PlayerState currentState { get; private set; }
    public void Initialize(PlayerState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(PlayerState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}

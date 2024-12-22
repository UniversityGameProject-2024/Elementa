using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected StateMachine stateMachine;
    private string animationName;
    protected float horizontalInput;

    public PlayerState(Player player, StateMachine stateMachine, string animationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationName = animationName;
    }

    public virtual void Enter()
    {
        Debug.Log("Player enter -" + animationName);
        player.animator.SetBool(animationName, true);
    }

    public virtual void Update()
    {
        Debug.Log("Player in -" + animationName);
        horizontalInput = player.GetHorizontalInput();
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animationName, false);
        Debug.Log("Player exit -" + animationName);
    }
}

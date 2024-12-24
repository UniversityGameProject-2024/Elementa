using UnityEngine;

public class PlayerState
{
    private string animationName;
    protected Player player;
    protected StateMachine stateMachine;
    protected float horizontalInput;
    protected Rigidbody2D body;
    protected GameObject gameObject;

    public PlayerState(Player player, StateMachine stateMachine, string animationName, GameObject gameObject)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationName = animationName;
        this.gameObject = gameObject;
    }

    public virtual void Enter()
    {
        Debug.Log("Player enter -" + animationName);
        player.animator.SetBool(animationName, true);
        body = player.body; 
    }

    public virtual void Update()
    {
        Debug.Log("Player in -" + animationName);
        horizontalInput = player.GetHorizontalInput();
        player.animator.SetFloat("y_velocity", body.linearVelocity.y);
    }

    public virtual void Exit()
    {
        Debug.Log("Player exit -" + animationName);
        player.animator.SetBool(animationName, false);
    }
}

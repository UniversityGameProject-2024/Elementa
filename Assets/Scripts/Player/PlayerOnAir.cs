using UnityEngine;

public class PlayerOnAir : PlayerState
{
    public PlayerOnAir(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.IsGroundDetect())
        {
            // Stop the player when landing
            player.Stand();
            stateMachine.ChangeState(player.idleState);
        }

        if(horizontalInput != 0)
        {
            player.SetRunSpeed(player.runSpeed * horizontalInput, body.linearVelocity.y);
        }
    }
}

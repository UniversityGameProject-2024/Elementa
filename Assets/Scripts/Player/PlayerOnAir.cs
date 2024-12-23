using UnityEngine;

public class PlayerOnAir : PlayerState
{
    public PlayerOnAir(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
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
            stateMachine.ChangeState(player.idleState);
        }

        if(horizontalInput != 0)
        {
            player.SetRunSpeed(player.runSpeed * horizontalInput, body.linearVelocity.y);
        }
    }
}

using UnityEngine;

public class PlayerMove : PlayerOnGround
{
    public PlayerMove(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
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

        player.SetRunSpeed(horizontalInput * player.runSpeed, body.linearVelocity.y);


        if (horizontalInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
using UnityEngine;

public class PlayerMove : PlayerOnGround
{
    public PlayerMove(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
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
using UnityEngine;

public class PlayerIdle : PlayerOnGround
{
    public PlayerIdle(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
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

        if ( horizontalInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}

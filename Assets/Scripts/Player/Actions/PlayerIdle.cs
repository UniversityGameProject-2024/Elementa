using UnityEngine;

public class PlayerIdle : PlayerOnGround
{
    public PlayerIdle(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.Stand();
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

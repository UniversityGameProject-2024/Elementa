using UnityEngine;

public class PlayerJump : PlayerState
{
    public PlayerJump(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        body.linearVelocity = new Vector2 (body.linearVelocity.x, player.jumpImpulse);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(body.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.onAir);
        }
    }
}

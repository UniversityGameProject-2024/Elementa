using UnityEngine;

public class PlayerJump : PlayerState
{
    public PlayerJump(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();
        body.linearVelocity = new Vector2(body.linearVelocity.x, player.jumpImpulse);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (body.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.onAir);
        }
    }
}

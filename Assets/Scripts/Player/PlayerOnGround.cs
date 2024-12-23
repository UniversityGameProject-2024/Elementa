using UnityEngine;

public class PlayerOnGround : PlayerState
{
    public PlayerOnGround(Player player, StateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
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

        if(Input.GetKeyDown(player._jumpKey) && player.IsGroundDetect())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (!player.IsGroundDetect())
        {
            stateMachine.ChangeState(player.onAir);
        }
    }
}

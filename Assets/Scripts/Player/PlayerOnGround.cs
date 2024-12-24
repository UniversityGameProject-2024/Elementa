using UnityEngine;

public class PlayerOnGround : PlayerState
{
    public PlayerOnGround(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Stand();
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.fireball);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(player.land);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.air); // Assuming player.airState is initialized
        }
    }
}

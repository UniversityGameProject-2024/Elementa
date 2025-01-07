using UnityEngine;

public class TrollAttacks : TrollGroundPosition
{
    public TrollAttacks(Enemy enemy, Troll troll, StateMachine stateMachine, string animationName, GameObject gameObject) : base(enemy, troll, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();
        if (player.position.x > troll.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < troll.transform.position.x)
        {
            moveDir -= 1;
        }
        troll.SetRunSpeed(1 * moveDir, body.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
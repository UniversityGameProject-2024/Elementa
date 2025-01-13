using UnityEngine;
using System.Collections;

public class TrollMove : TrollGroundPosition
{
    public TrollMove(Enemy enemy, Troll troll, StateMachine stateMachine, string animationName, GameObject gameObject) : base(enemy, troll, stateMachine, animationName, gameObject)
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
        troll.SetRunSpeed(troll.enemySpeed * troll.viewDirection, troll.body.linearVelocity.y);

        if (troll.IsWallDetected() || !troll.IsGroundDetect())
        {
            troll.FlipPlayer();
            stateMachine.ChangeState(troll.trollIdle);
        }
    }
}

using System;
using UnityEngine;

public class TrollGroundPosition : EnemyState
{
    protected Troll troll;
    protected Transform player;
    protected int moveDir;
    public TrollGroundPosition(Enemy enemy, Troll troll, StateMachine stateMachine, string animationName, GameObject gameObject) : base(enemy, stateMachine, animationName, gameObject)
    {
        this.troll = troll;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void IsAnimFinshed()
    {
        base.IsAnimFinshed();
    }

    public override void Update()
    {
        base.Update();

        //if (player.position.x > troll.transform.position.x)
        //{
        //    moveDir = 1;
        //}
        //else if (player.position.x < troll.transform.position.x)
        //{
        //    moveDir -= 1;
        //}
        //troll.SetRunSpeed(troll.enemySpeed * moveDir, body.linearVelocity.y);
        if (troll.DetectPlayer())
        {
            stateMachine.ChangeState(troll.trollAttacks);
        }
    }
}

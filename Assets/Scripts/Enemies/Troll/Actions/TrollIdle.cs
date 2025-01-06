using UnityEngine;

public class TrollIdle : TrollGroundPosition
{
    public TrollIdle(Enemy enemy, Troll troll, StateMachine stateMachine, string animationName, GameObject gameObject) : base(enemy, troll, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyTimer = troll.idleTimner;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemyTimer < 0f)
        {
            stateMachine.ChangeState(troll.trollMove);
        }
    }
}

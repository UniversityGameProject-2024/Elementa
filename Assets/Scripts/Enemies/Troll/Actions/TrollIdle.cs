using UnityEngine;

public class TrollIdle : EnemyState
{
    private Troll troll;
    public TrollIdle(Enemy enemy, Troll troll, StateMachine stateMachine, string animationName, GameObject gameObject) : base(troll, stateMachine, animationName, gameObject)
    {
        this.troll = troll;
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

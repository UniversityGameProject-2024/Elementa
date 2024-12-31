using UnityEngine;

public class TrollMove : EnemyState
{
    private Troll troll;
    public TrollMove(Enemy enemy, Troll troll, StateMachine stateMachine, string animationName, GameObject gameObject) : base(enemy, stateMachine, animationName, gameObject)
    {
        this.troll = troll;
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
    }
}

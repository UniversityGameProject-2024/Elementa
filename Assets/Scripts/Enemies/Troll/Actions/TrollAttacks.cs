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
    }

}

using UnityEngine;

public class Troll : Enemy
{
    #region States
    public TrollIdle trollIdle {  get; private set; }
    //public TrollMove trollMove { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        trollIdle = new TrollIdle(this, this, stateMachine, "idle", null);
        //trollMove = new TrollMove(this, this, stateMachine, "move", null);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(trollIdle);
    }

    protected override void Update()
    {
        base.Update();
    }
}

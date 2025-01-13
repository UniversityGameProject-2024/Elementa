using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask playerLayerMask;

    [Header("Enemy control")]
    public float enemySpeed;
    public float idleTimner;
    public float distance;
    public StateMachine stateMachine { get; private set; }
    [Header("Attack control")]
    public float attackDist;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.enemyState.Update();
    }

    public virtual RaycastHit2D DetectPlayer() => Physics2D.Raycast(wallCheck.position, Vector2.right * viewDirection, distance, playerLayerMask);
}

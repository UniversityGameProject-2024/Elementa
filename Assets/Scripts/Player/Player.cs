using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region States
    public StateMachine stateMachine { get; private set; }
    public PlayerIdle idleState { get; private set; }
    public PlayerMove moveState { get; private set; }
    public PlayerOnGround onGround { get; private set; }
    public PlayerOnAir onAir { get; private set; }
    public PlayerJump jumpState { get; private set; }
    #endregion

    #region Compnents
    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    #endregion

    #region SerializeField
    [Tooltip("Player Speed")]
    [SerializeField] public float runSpeed = 5;

    public float jumpImpulse;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDist;
    [SerializeField] private LayerMask groundLayer;

    [Header("Input Customization")]
    [Tooltip("Key for moving left")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;
    [Tooltip("Key for moving right")]
    [SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow;
    [Tooltip("Key for jump")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    public KeyCode _jumpKey => jumpKey;// Public property to provide read-only access
    #endregion
    public int viewDirection { get; private set; } = 1;
    private bool rightView = true;


    private void Awake()
    {
        stateMachine = new StateMachine();
        idleState = new PlayerIdle(this, stateMachine, "idle");
        moveState = new PlayerMove(this, stateMachine, "run");
        onAir = new PlayerOnAir(this, stateMachine, "jump");
        jumpState = new PlayerJump(this, stateMachine, "jump");
    }

    private void Start()
    {
        //Grab ref for rigidbody and animator from object
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        stateMachine.Initialize(idleState);
    }

    public void SetRunSpeed(float xSpeed, float ySpeed)
    {
        body.linearVelocity = new Vector2(xSpeed, ySpeed);
    }
    public void Stand() => body.linearVelocity = new Vector2(0, 0);


    public float GetHorizontalInput()
    {
        if (Input.GetKey(moveLeftKey)) return -1f;
        if (Input.GetKey(moveRightKey)) return 1f;
        return 0f;
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        FlipControl();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundDist));
    }

    public bool IsGroundDetect() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDist, groundLayer);

    public void FlipPlayer()
    {
        viewDirection = viewDirection * (-1);
        rightView = !rightView;
        transform.Rotate(0, 180, 0);
    }

    public void FlipControl()
    {
        if(body.linearVelocity.x > 0 && !rightView)
        {
            FlipPlayer();
        }
        else if(body.linearVelocity.x < 0 && rightView)
        {
            FlipPlayer();
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    #region States
    public StateMachine stateMachine { get; private set; }
    public PlayerIdle idleState { get; private set; }
    public PlayerMove moveState { get; private set; }
    public PlayerOnGround onGround { get; private set; }
    public PlayerOnAir onAir { get; private set; }
    public PlayerJump jumpState { get; private set; }
    public Fireball fireball { get; private set; }
    public Land land { get; private set; }
    public Air air { get; private set; }


    #endregion

    #region Compnents
    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }
    #endregion

    #region SerializeField
    [Header("Player Settings")]
    [Tooltip("Player Speed")]
    [SerializeField] public float runSpeed = 5;

    [SerializeField] public float jumpImpulse;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private float groundDist;
    [SerializeField] private LayerMask groundLayer;

    [Header("Input Customization")]
    [Tooltip("Key for moving left")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;
    [Tooltip("Key for moving right")]
    [SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow;
    [Tooltip("Key for jump")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [Tooltip("Key for Shooting a fireball")]
    [SerializeField] private KeyCode fireballKey = KeyCode.Alpha2;
    [Tooltip("Key for creating a land tile")]
    [SerializeField] private KeyCode landTileKey = KeyCode.Alpha3;
    [Tooltip("Key for a air spell")]
    [SerializeField] private KeyCode airKey = KeyCode.Alpha4;
    public KeyCode _jumpKey => jumpKey;// Public property to provide read-only access
    public KeyCode _fireballKey => fireballKey;// Public property to provide read-only access
    public KeyCode _landTileKey => landTileKey;// Public property to provide read-only access
    public KeyCode _airKey => airKey;// Public property to provide read-only access
    #endregion
    public int viewDirection { get; private set; } = 1;
    private bool rightView = true;

    #region Magic Prefabs
    [Header("Magic Actions")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] public float fireballSpeed; // Speed of the fireball
    [SerializeField] public GameObject landPrefab;
    [SerializeField] public GameObject airPrefab;
    [SerializeField] public Transform shootPoint;// The position from where the fireball spawns

    #endregion

    private void Awake()
    {
        stateMachine = new StateMachine();
        idleState = new PlayerIdle(this, stateMachine, "idle", null);
        moveState = new PlayerMove(this, stateMachine, "run", null);
        onAir = new PlayerOnAir(this, stateMachine, "jump", null);
        jumpState = new PlayerJump(this, stateMachine, "jump", null);
        fireball = new Fireball(this, stateMachine, "fireball", fireballPrefab);
        land = new Land(this, stateMachine, "land", landPrefab);
        air = new Air(this, stateMachine, "air", airPrefab);

    }

    private void Start()
    {
        //Grab ref for rigidbody and animator from object
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
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

    public virtual bool IsGroundDetect() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDist, groundLayer);

    public void FlipPlayer()
    {
        viewDirection = viewDirection * (-1);
        rightView = !rightView;
        transform.Rotate(0, 180, 0);
    }


    public void FlipControl()
    {
        const float tolerance = 0.1f; // Add a small tolerance to prevent unnecessary flipping

        if (Mathf.Abs(body.linearVelocity.x) > tolerance) // Only flip if velocity exceeds tolerance
        {
            if (body.linearVelocity.x > 0 && !rightView)
            {
                FlipPlayer();
            }
            else if (body.linearVelocity.x < 0 && rightView)
            {
                FlipPlayer();
            }
        }
    }

}


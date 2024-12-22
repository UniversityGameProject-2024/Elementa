using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }

    public PlayerIdle idleState { get; private set; }

    public PlayerMove moveState { get; private set; }

    public Animator animator { get; private set; }

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;

    [Tooltip("Player Speed")]

    [SerializeField] public float runSpeed = 5;

    [SerializeField] private LayerMask groundLayer;

    [Header("Input Customization")]
    [Tooltip("Key for moving left")]
    [SerializeField] private KeyCode moveLeftKey = KeyCode.LeftArrow;
    [Tooltip("Key for moving right")]
    [SerializeField] private KeyCode moveRightKey = KeyCode.RightArrow;
    private void Awake()
    {
        stateMachine = new StateMachine();
        idleState = new PlayerIdle(this, stateMachine, "idle");
        moveState = new PlayerMove(this, stateMachine, "run");
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


    public float GetHorizontalInput()
    {
        if (Input.GetKey(moveLeftKey)) return -1f;
        if (Input.GetKey(moveRightKey)) return 1f;
        return 0f;
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}

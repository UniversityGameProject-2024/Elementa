using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : Entity
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
    public Water water { get; private set; }


    #endregion

    #region SerializeField
    [Header("Player Settings")]
    [Tooltip("Player Speed")]
    [SerializeField] public float runSpeed = 5;
    [SerializeField] public float jumpImpulse;

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
    [Tooltip("Key for a Water Shield")]
    [SerializeField] private KeyCode watershieldKey = KeyCode.Alpha1;

    public KeyCode _jumpKey => jumpKey;// Public property to provide read-only access
    public KeyCode _fireballKey => fireballKey;// Public property to provide read-only access
    public KeyCode _landTileKey => landTileKey;// Public property to provide read-only access
    public KeyCode _airKey => airKey;// Public property to provide read-only access
    public KeyCode _watershildKey => watershieldKey;// Public property to provide read-only access

    #endregion

    #region Magic Prefabs
    [Header("Magic Actions")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] public float fireballSpeed; // Speed of the fireball
    [SerializeField] public GameObject landPrefab;
    [SerializeField] public GameObject airPrefab;
    [SerializeField] public GameObject waterPrefab;
    [SerializeField] public Transform shootPoint;// The position from where the fireball spawns

    #endregion
    private HealthManager healthManager;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine();
        idleState = new PlayerIdle(this, stateMachine, "idle", null);
        moveState = new PlayerMove(this, stateMachine, "run", null);
        onAir = new PlayerOnAir(this, stateMachine, "jump", null);
        jumpState = new PlayerJump(this, stateMachine, "jump", null);
        fireball = new Fireball(this, stateMachine, "attack", fireballPrefab);
        land = new Land(this, stateMachine, "attack", landPrefab);
        air = new Air(this, stateMachine, "attack", airPrefab);
        water = new Water(this, stateMachine, "attack", waterPrefab);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        // Initialize health
        healthManager = FindFirstObjectByType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.InitializeHealth(6); // Starts with 2 full hearts and 1 half heart
        }
    }

    public void Stand() => body.linearVelocity = new Vector2(0, 0);


    public float GetHorizontalInput()
    {
        if (Input.GetKey(moveLeftKey)) return -1f;
        if (Input.GetKey(moveRightKey)) return 1f;
        return 0f;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        FlipControl();
    }
    public void AnimTrigger() => stateMachine.currentState.IsAnimFinshed();
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Stop player movement
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

}


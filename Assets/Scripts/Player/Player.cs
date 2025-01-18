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
    [Header("Audio")]
    [SerializeField] public AudioClip fireballSound;
    [SerializeField] public AudioClip waterSound;
    [SerializeField] public AudioClip airSound;
    [SerializeField] public AudioClip landlSound;
    [SerializeField] public AudioClip jumpSound;

    public KeyCode _jumpKey => jumpKey;// Public property to provide read-only access
    public KeyCode _fireballKey => fireballKey;// Public property to provide read-only access
    public KeyCode _landTileKey => landTileKey;// Public property to provide read-only access
    public KeyCode _airKey => airKey;// Public property to provide read-only access
    public KeyCode _watershildKey => watershieldKey;// Public property to provide read-only access

    [Header("Magic Action Durations")]
    [SerializeField] private float waterShieldDuration = 2f; // Customizable shield duration in seconds
    public float WaterShieldDuration => waterShieldDuration; // Public property to access the duration
    [Header("Spell Cooldowns")]
    [SerializeField] private float fireballCooldown = 3f;
    [SerializeField] private float airCooldown = 5f;
    [SerializeField] private float landCooldown = 4f;
    [SerializeField] private float waterCooldown = 6f;

    [SerializeField] private SpellCooldownBar fireballCooldownBar;
    [SerializeField] private SpellCooldownBar airCooldownBar;
    [SerializeField] private SpellCooldownBar landCooldownBar;
    [SerializeField] private SpellCooldownBar waterCooldownBar;
    private float fireballTimer = 0f;
    private float airTimer = 0f;
    private float landTimer = 0f;
    private float waterTimer = 0f;
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
        water = new Water(this, stateMachine, "attack", waterPrefab); // Initialize water state with the prefab
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    private void UpdateCooldownTimers()
    {
        // Reduce cooldown timers over time
        if (fireballTimer > 0) fireballTimer -= Time.deltaTime;
        if (airTimer > 0) airTimer -= Time.deltaTime;
        if (landTimer > 0) landTimer -= Time.deltaTime;
        if (waterTimer > 0) waterTimer -= Time.deltaTime;
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
        // Update spell cooldown timers
        UpdateCooldownTimers();
    }
    public bool CanCastFireball() => fireballCooldownBar.IsCooldownComplete();
    public bool CanCastAir() => airCooldownBar.IsCooldownComplete();
    public bool CanCastLand() => landCooldownBar.IsCooldownComplete();
    public bool CanCastWater() => waterCooldownBar.IsCooldownComplete();

    public void StartFireballCooldown() => fireballCooldownBar.StartCooldown(fireballCooldown);
    public void StartAirCooldown() => airCooldownBar.StartCooldown(airCooldown);
    public void StartLandCooldown() => landCooldownBar.StartCooldown(landCooldown);
    public void StartWaterCooldown() => waterCooldownBar.StartCooldown(waterCooldown);

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


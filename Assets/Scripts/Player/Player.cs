using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System.Collections.Generic;

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
    [SerializeField]
    [Tooltip("Keys for moving left")]
    private List<KeyCode> moveLeftKeys = new List<KeyCode> { KeyCode.LeftArrow };

    [SerializeField]
    [Tooltip("Keys for moving right")]
    private List<KeyCode> moveRightKeys = new List<KeyCode> { KeyCode.RightArrow };

    [SerializeField]
    [Tooltip("Keys for jumping")]
    private List<KeyCode> jumpKeys = new List<KeyCode> { KeyCode.Space };
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

    [SerializeField] private Camera mainCamera; // Reference to the main camera

    //public KeyCode _jumpKey => jumpKey;// Public property to provide read-only access
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
    [SerializeField] private TextMeshProUGUI airGuideText;
    [SerializeField] public GameObject waterPrefab;
    [SerializeField] public Transform shootPoint;// The position from where the fireball spawns

    #endregion
    [SerializeField] private LineRenderer lineRenderer; // Reference to the LineRenderer

    [Header("Line Settings")]
    [SerializeField] public float maxControlDistance = 5f; // Maximum distance the player can move the land object
    [SerializeField] private float lineStartWidth = 0.15f; // Start width of the line
    [SerializeField] private float lineEndWidth = 0.1f;    // End width of the line
    [SerializeField] private Color lineStartColor = Color.cyan; // Start color
    [SerializeField] private Color lineEndColor = Color.blue;   // End color


    // Store the default camera size
    private float defaultCameraSize;

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
        air = new Air(this, stateMachine, "attack", airPrefab, airGuideText);
        water = new Water(this, stateMachine, "attack", waterPrefab); // Initialize water state with the prefab
        if (mainCamera == null)
            mainCamera = Camera.main;

        defaultCameraSize = mainCamera.orthographicSize;

        // Ensure LineRenderer is attached
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Apply settings from Inspector
        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;
        lineRenderer.startColor = lineStartColor;
        lineRenderer.endColor = lineEndColor;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // Initially hidden
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
        // Check if any key in moveLeftKeys is pressed
        foreach (var key in moveLeftKeys)
        {
            if (Input.GetKey(key)) return -1f;
        }

        // Check if any key in moveRightKeys is pressed
        foreach (var key in moveRightKeys)
        {
            if (Input.GetKey(key)) return 1f;
        }

        return 0f;
    }
    public bool IsJumpKeyPressed()
    {
        // Check if any key in jumpKeys is pressed
        foreach (var key in jumpKeys)
        {
            if (Input.GetKeyDown(key)) return true;
        }

        return false;
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


    // Function to change the camera size
    public void ChangeCameraSize(float newSize)
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = newSize;
        }
    }

    // Function to reset camera size to default
    public void ResetCameraSize()
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = defaultCameraSize;
        }
    }

    // Function to update the line position
    public void UpdateLine(Vector3 targetPosition)
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPosition);
        }
    }

    // Function to hide the line
    public void HideLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}


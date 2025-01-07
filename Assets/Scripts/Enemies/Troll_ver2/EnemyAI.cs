using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float idleTime = 2f;
    public float attackCooldown = 2f; // Time between attacks
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public int damage = 1; // Damage dealt per attack (half a heart)
    public LayerMask playerLayer;

    private float idleTimer = 0f;
    private float attackTimer = 0f; // Timer for attack cooldown
    private bool isFacingRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private enum State { Idle, Move, Attack }
    private State currentState = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component
        idleTimer = idleTime;
    }

    void Update()
    {
        // State behavior
        switch (currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Move:
                HandleMove();
                break;
            case State.Attack:
                HandleAttack();
                break;
        }

        // Detect the player
        DetectPlayer();
    }

    private void HandleIdle()
    {
        idleTimer -= Time.deltaTime;
        animator.SetBool("Idle", true); // Set Idle animation
        animator.SetBool("Run", false); // Ensure Run animation is off

        if (idleTimer <= 0f)
        {
            currentState = State.Move;
            animator.SetBool("Idle", false); // Stop Idle animation
        }
    }

    private void HandleMove()
    {
        // Move in the facing direction
        float direction = isFacingRight ? 1 : -1;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("Run", true); // Play Run animation
        animator.SetBool("Idle", false); // Stop Idle animation

        // Check for walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.5f);
        if (hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            Flip();
            currentState = State.Idle;
            idleTimer = idleTime;
            animator.SetBool("Run", false); // Stop Run animation
            animator.SetBool("Idle", true); // Play Idle animation
        }
    }

    private void HandleAttack()
    {
        rb.linearVelocity = Vector2.zero; // Stop movement
        animator.SetTrigger("Attack"); // Play Attack animation
        Debug.Log("Attacking Player!");
        // Reduce player's health
        if (attackTimer >= attackCooldown && player != null)
        {
            // Reduce player's health
            HealthManager healthManager = player.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.ReduceHealth(damage);
            }

            attackTimer = 0f; // Reset attack cooldown
        }

        // Transition back to movement if the player moves out of range
        if (player == null || Vector2.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Idle;
            idleTimer = 0f;
        }
    }

    private void DetectPlayer()
    {
        Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (playerInRange != null)
        {
            player = playerInRange.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                currentState = State.Attack;
            }
            else
            {
                currentState = State.Move;
                ChasePlayer();
            }
        }
        else
        {
            player = null;
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

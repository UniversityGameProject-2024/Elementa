using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundDist;
    [SerializeField] protected float wallDist;
    [SerializeField] protected LayerMask groundLayer;
    public int viewDirection { get; private set; } = 1;
    protected bool rightView = true;


    #region Compnents
    public Animator animator { get; private set; }
    public Rigidbody2D body { get; private set; }
    #endregion
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        //Grab ref for rigidbody and animator from object
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual bool IsGroundDetect() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDist, groundLayer);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * viewDirection, wallDist, groundLayer);


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundDist));
    }

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
    public void SetRunSpeed(float xSpeed, float ySpeed)
    {
        body.linearVelocity = new Vector2(xSpeed, ySpeed);
    }
}

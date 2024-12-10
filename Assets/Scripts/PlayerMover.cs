using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    [Tooltip("Player Speed")]
    [SerializeField] private float speed = 5;

    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        //Grab ref for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //Flip player when moving left-right
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && IsGround())
        {
            Jump();
        }

        animator.SetBool("run", horizontalInput != 0);//Set animator parameters
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
    }

    private bool IsGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,
            0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGround();
    }

}

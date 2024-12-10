using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D body;

    [Tooltip("Player Speed")]
    [SerializeField] private float speed = 5;

    private Animator animator;

    private void Awake()
    {
        //Grab ref for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput  * speed, body.linearVelocity.y);

        //Flip player when moving left-right
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        }

        //Set animator parameters
        animator.SetBool("run", horizontalInput != 0); 
    }

}

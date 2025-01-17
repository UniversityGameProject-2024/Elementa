using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform startPoint; // Reference to the start point GameObject
    [SerializeField] private Transform endPoint;   // Reference to the end point GameObject
    [SerializeField] private float speed = 2f;     // Speed of the platform
    [SerializeField] private bool loop = true;     // Whether the platform loops back and forth

    private Transform targetPoint; // Current target point

    private void Start()
    {
        // Initialize the platform position and set the initial target
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }

        targetPoint = endPoint;
    }

    private void Update()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogWarning("StartPoint or EndPoint is not assigned!");
            return;
        }

        // Move the platform towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.01f)
        {
            // Reverse the direction if looping is enabled
            if (loop)
            {
                targetPoint = targetPoint == startPoint ? endPoint : startPoint;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (startPoint != null && endPoint != null)
        {
            // Draw gizmos to visualize the platform's path
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
            Gizmos.DrawSphere(startPoint.position, 0.1f);
            Gizmos.DrawSphere(endPoint.position, 0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is on the platform
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform); // Attach player to platform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Detach the player when they leave the platform
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null); // Detach player from platform
        }
    }
}

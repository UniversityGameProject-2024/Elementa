using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Speed at which the enemy moves
    [SerializeField] private float moveDistance = 5f; // How far the enemy will move before reversing direction
    private Vector2 startPosition; // Initial position of the enemy
    private Vector2 targetPosition; // Current target position to move towards

    private void Start()
    {
        // Set the start position
        startPosition = transform.position;
        // Set the initial target position
        targetPosition = new Vector2(startPosition.x + moveDistance, startPosition.y);
    }

    private void Update()
    {
        // Move the enemy towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if ((Vector2)transform.position == targetPosition)
        {
            // Reverse direction
            targetPosition = targetPosition.x == startPosition.x + moveDistance
                ? new Vector2(startPosition.x - moveDistance, startPosition.y)
                : new Vector2(startPosition.x + moveDistance, startPosition.y);
        }
    }
}

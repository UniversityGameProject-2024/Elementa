using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab; // Reference to the fire prefab
    [SerializeField] private GameObject WaterballPrefab; // Reference to the water prefab
    [SerializeField] private GameObject TornadoPrefab; // Reference to the air prefab

    [SerializeField] private Transform ShootPoint; // The point where the fireball is spawned
    [SerializeField] private float ShootSpeed = 10f; // Speed of the fireball
    [SerializeField] private float shootCooldown = 0.5f; // Cooldown time between shots in seconds

    private Vector2 facingDirection = Vector2.right; // Default facing direction is right
    private float timeSinceLastShot = 0f; // Timer to track time since the last shot

    private void Update()
    {
        // Update the facing direction based on arrow key input
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            facingDirection = Vector2.right;
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            facingDirection = Vector2.left;
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            facingDirection = Vector2.up;
        }

        // Update the timer and check if enough time has passed to shoot again
        timeSinceLastShot += Time.deltaTime;

        // Shoot fireball when "1" key is pressed and cooldown has passed
        if (Keyboard.current.digit1Key.wasPressedThisFrame && timeSinceLastShot >= shootCooldown)
        {
            ShootFireball(facingDirection, fireballPrefab);
            timeSinceLastShot = 0f; // Reset the timer after shooting
        }
        // Shoot waterball when "2" key is pressed and cooldown has passed
        else if (Keyboard.current.digit2Key.wasPressedThisFrame && timeSinceLastShot >= shootCooldown)
        {
            ShootFireball(facingDirection, WaterballPrefab);
            timeSinceLastShot = 0f; // Reset the timer after shooting
        }
        // Shoot another waterball when "3" key is pressed and cooldown has passed
        else if (Keyboard.current.digit3Key.wasPressedThisFrame && timeSinceLastShot >= shootCooldown)
        {
            ShootFireball(facingDirection, TornadoPrefab);
            timeSinceLastShot = 0f; // Reset the timer after shooting
        }
    }

    private void ShootFireball(Vector2 direction, GameObject ballPrefab)
    {
        // Calculate the rotation angle based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantiate the fireball with the correct rotation
        GameObject fireball = Instantiate(ballPrefab, ShootPoint.position, Quaternion.Euler(0, 0, angle));

        // Get the Rigidbody2D component and set velocity
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * ShootSpeed; // Corrected to use 'velocity' instead of 'linearVelocity'
        }
    }
}

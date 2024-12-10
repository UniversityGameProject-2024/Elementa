using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f; // Time before the fireball disappears
    //[SerializeField] private int damage = 10; // Damage the fireball deals

    private void Start()
    {
        // Destroy the fireball after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with enemies
        if (collision.CompareTag("Enemy"))
        {
            // Check if this is a fireball projectile (can add more checks for other projectiles if needed)
            if (CompareTag("Fireball"))
            {
                // Destroy the enemy when hit by fireball
                Destroy(collision.gameObject); // Destroy the enemy
            }

            // Destroy the projectile itself
            Destroy(gameObject);
        }

        if (collision.CompareTag("AirObstacle"))
        {
            // Check if this is a fireball projectile (can add more checks for other projectiles if needed)
            if (CompareTag("Tornado"))
            {
                // Destroy the enemy when hit by fireball
                Destroy(collision.gameObject); // Destroy the enemy
            }

            // Destroy the projectile itself
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            // Destroy the projectile itself
            Destroy(gameObject);
        }
    }
}

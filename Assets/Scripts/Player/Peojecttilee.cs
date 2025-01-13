using UnityEngine;
using System.Collections;

public class Projectilee : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f; // Time before the fireball disappears
    [SerializeField] private int fireball_damage = 1; // Damage the fireball deals

    private void Start()
    {
        // Destroy the fireball after its lifetime expires
        StartCoroutine(HandleLifetime());
    }

    private IEnumerator HandleLifetime()
    {
        // Wait for the projectile's lifetime to expire
        yield return new WaitForSeconds(lifetime);

        // Check if this is a Tornado projectile
        if (CompareTag("Air"))
        {
            destroyAirSpell();
        }

        // Destroy the projectile itself
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with enemies
        if (collision.CompareTag("Tree") && collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            if (CompareTag("Air"))
            {
                destroyAirSpell();

            }
            // Destroy the projectile itself
            if (CompareTag("Fireball"))
            {
                collision.GetComponent<Health>().TakeDamage(fireball_damage);
                //Destroy(collision.gameObject); // Destroy the enemy
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Land"))
        {
            if (CompareTag("Air"))
            {
                // Notify the player to control the land object
                Player player = FindAnyObjectByType<Player>();
                if (player != null && player.stateMachine.currentState is Air airState)
                {
                    airState.ControlLand(collision.gameObject);
                }
                // Destroy the projectile itself
                Destroy(gameObject);
            }
        }
    }


    private void destroyAirSpell()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player != null && player.stateMachine.currentState is Air airState)
        {
            airState.returnPlayer();
        }
    }
}

using UnityEngine;
using System.Collections;

public class WatershildHandle : MonoBehaviour
{
    private float shieldDuration;

    private void Start()
    {
        // Find the Player instance
        Player player = FindAnyObjectByType<Player>();

        if (player != null)
        {
            // Get the shield duration from the player
            shieldDuration = player.WaterShieldDuration;

            // Start the lifetime coroutine
            StartCoroutine(HandleLifetime());
        }
        else
        {
            Debug.LogError("Player not found. WatershieldHandle cannot get shield duration.");
        }
    }

    private IEnumerator HandleLifetime()
    {
        // Wait for the shield duration to expire
        yield return new WaitForSeconds(shieldDuration);

        // Destroy the shield object
        Destroy(gameObject);
    }
}

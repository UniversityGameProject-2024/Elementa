using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint updated: " + transform.position);
            }
        }
    }
}
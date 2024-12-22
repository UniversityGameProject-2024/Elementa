using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CollectableCoin : MonoBehaviour
{
    public TextMeshProUGUI successText;  // Reference to TextMeshPro text component

    private void Start()
    {
        // Hide the SUCCESS text initially
        if (successText != null)
        {
            successText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the coin
        if (collision.CompareTag("Player"))
        {
            // Show "SUCCESS" text
            if (successText != null)
            {
                successText.gameObject.SetActive(true);
                successText.text = "SUCCESS!";
            }

            // Destroy the coin
            Destroy(gameObject);
        }
    }

}

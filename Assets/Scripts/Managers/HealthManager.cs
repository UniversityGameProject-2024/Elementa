using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image[] heartImages; // Assign heart images in the Inspector
    [SerializeField] private Sprite fullHeart;   // Sprite for full heart
    [SerializeField] private Sprite halfHeart;   // Sprite for half heart
    [SerializeField] private Sprite emptyHeart;  // Sprite for empty heart

    private int maxHealth;

    // Initializes the hearts with a specific amount of health
    public void InitializeHealth(int initialHealth)
    {
        maxHealth = heartImages.Length * 2; // Assuming each heart represents 2 health units
        UpdateHealthUI(initialHealth, maxHealth);
    }

    // Updates the UI to reflect the current health
    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            int heartHealth = currentHealth - (i * 2); // Calculate health for each heart (2 units per heart)

            if (heartHealth >= 2)
            {
                heartImages[i].sprite = fullHeart; // Full heart
            }
            else if (heartHealth == 1)
            {
                heartImages[i].sprite = halfHeart; // Half heart
            }
            else
            {
                heartImages[i].sprite = emptyHeart; // Empty heart
            }
        }
    }
}


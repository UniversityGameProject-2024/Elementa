using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ManagerCoin : MonoBehaviour
{
    public static ManagerCoin Instance;    // Singleton instance for easy access
    public TextMeshProUGUI successText;    // Reference to the UI text component
    private int coinCount = 0;             // Total coins collected
    [SerializeField] private float CoinShowTextTime;

    private void Awake()
    {
        // Ensure there's only one ManagerCoin instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Hide the text initially
        if (successText != null)
        {
            successText.gameObject.SetActive(false);
        }
    }

    public void CoinCollected()
    {
        // Increment the coin count
        coinCount++;

        // Update and display the text
        if (successText != null)
        {
            successText.text = "Coins Collected: " + coinCount;
            successText.gameObject.SetActive(true);

            // Start the coroutine to hide the text after 3 seconds
            StartCoroutine(HideTextAfterDelay(CoinShowTextTime));
        }
    }

    private System.Collections.IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (successText != null && successText.gameObject.activeSelf)
        {
            successText.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using TMPro;

public class GuideTrigger : MonoBehaviour
{
    [SerializeField] private string guideMessage;        // Message to display
    [SerializeField] private TextMeshProUGUI guideText;  // Reference to TextMeshProUGUI
    [SerializeField] private GameObject guideBackground; // Refernce to the background GameObject


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Show the guide panel with the message
            guideText.text = guideMessage;
            guideBackground.SetActive(true);
            guideText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Hide the guide panel when the player exits
            guideBackground.SetActive(false);
            guideText.gameObject.SetActive(false);
        }
    }
}

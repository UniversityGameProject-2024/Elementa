using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private string guideMessage;        // Message to display
    [SerializeField] private TextMeshProUGUI guideText;  // Reference to TextMeshProUGUI
    [SerializeField] private GameObject guideBackground; // Refernce to the background GameObject
    [Header("Audio")]
    [SerializeField] private AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(sound);
            int currentCoins = ManagerCoin.Instance.GetCoinCount();
            // Show the guide panel with the message
            guideText.text = guideMessage + "\nCoins Collected:\n " + currentCoins + " / 12";
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

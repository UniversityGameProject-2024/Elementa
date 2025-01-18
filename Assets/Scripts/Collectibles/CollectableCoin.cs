using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Notify ManagerCoin to update UI and handle coin count
            ManagerCoin.Instance.CoinCollected();
            SoundManager.instance.PlaySound(sound);
            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}

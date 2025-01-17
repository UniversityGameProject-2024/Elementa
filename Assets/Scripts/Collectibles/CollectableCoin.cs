using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Notify ManagerCoin to update UI and handle coin count
            ManagerCoin.Instance.CoinCollected();

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header("Audio")]
    [SerializeField] private AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            SoundManager.instance.PlaySound(sound);
            gameObject.SetActive(false);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldownBar : MonoBehaviour
{
    [SerializeField] private Image cooldownBar; // Reference to the Image component for the cooldown
    private float cooldownDuration; // Duration of the cooldown in seconds
    private float cooldownTimer;

    private void Start()
    {
        // Initialize the cooldown bar to be fully filled
        cooldownBar.fillAmount = 1;
        cooldownTimer = 0;
    }

    private void Update()
    {
        // Update the cooldown bar if a cooldown is active
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownBar.fillAmount = 1 - (cooldownTimer / cooldownDuration);
        }
    }

    // Method to start the cooldown with a given duration
    public void StartCooldown(float duration)
    {
        cooldownDuration = duration;
        cooldownTimer = cooldownDuration;
        cooldownBar.fillAmount = 0; // Reset the cooldown bar to empty
    }

    // Method to check if the cooldown is complete
    public bool IsCooldownComplete()
    {
        return cooldownTimer <= 0;
    }
}

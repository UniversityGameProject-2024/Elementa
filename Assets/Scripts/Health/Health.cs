using UnityEngine;
using System.Collections;
using TMPro;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private Vector3 checkpointPosition;
    [SerializeField] private bool isPlayer;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurthSound;

    // Reference to the player
    private Player player;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        checkpointPosition = transform.position; // Set the initial checkpoint to the starting position

        // Get the Player component if this is the player object
        if (isPlayer)
        {
            player = GetComponent<Player>();
        }

    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            if (isPlayer)
            {
                // Check if the player is in the Air state
                if (player.stateMachine.currentState is Air airState)
                {
                    airState.returnPlayer();
                }
            }
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurthSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
                if (isPlayer)
                {
                    // Check if the player is in the Air state
                    if (player.stateMachine.currentState is Air airState)
                    {
                        airState.returnPlayer();
                    }
                    Invoke(nameof(Respawn), 2.0f); // Add a delay before respawning
                }
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(6, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 9, false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void SetInvulnerability(bool state)
    {
        invulnerable = state;
        Physics2D.IgnoreLayerCollision(6, 9, state);
    }
    //Respawn
    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    public void Respawn()
    {
        transform.position = checkpointPosition; // Move the player to the checkpoint
        currentHealth = startingHealth; // Restore health
        anim.ResetTrigger("die");
        anim.Play("Idle");
        dead = false;
        Debug.Log("Player Respawn");

        foreach (Behaviour component in components)
            component.enabled = true;

        StartCoroutine(Invunerability()); // Apply invulnerability after respawn
    }
}
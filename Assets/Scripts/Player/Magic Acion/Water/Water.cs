using UnityEngine;

public class Water : PlayerState
{
    private GameObject currentShield;
    private float shieldDuration;
    private float shieldTimer;

    public Water(Player player, StateMachine stateMachine, string animationName, GameObject gameObject)
        : base(player, stateMachine, animationName, gameObject)
    {
        shieldDuration = player.WaterShieldDuration; // Set the duration from the player's property
    }

    public override void Enter()
    {
        if (!player.CanCastWater())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        base.Enter();

        // Start cooldown
        player.StartWaterCooldown();
        SoundManager.instance.PlaySound(player.waterSound);


        // Stop the player's movement
        player.Stand();

        // Instantiate the water shield and parent it to the player
        if (gameObject != null)
        {
            currentShield = Object.Instantiate(gameObject, player.transform.position, Quaternion.identity);
            currentShield.transform.SetParent(player.transform); // Make the shield follow the player

            // Make the player invulnerable
            player.GetComponent<Health>().SetInvulnerability(true);
        }

        // Initialize the shield timer
        shieldTimer = shieldDuration;
    }

    public override void Update()
    {
        base.Update();

        // Ensure the player doesn't move while the shield is active
        player.Stand();

        // Reduce the timer each frame
        shieldTimer -= Time.deltaTime;

        // If the timer reaches zero, deactivate the shield
        if (shieldTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        // Destroy the shield when exiting the state
        if (currentShield != null)
        {
            Object.Destroy(currentShield);
        }

        // Remove invulnerability
        player.GetComponent<Health>().SetInvulnerability(false);
    }
}

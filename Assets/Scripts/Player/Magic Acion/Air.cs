using UnityEngine;

public class Air : PlayerState
{
    private GameObject currentAirProjectile;
    private GameObject controlledLandObject;

    public Air(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        if (!player.CanCastAir())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        base.Enter();

        // Start cooldown
        player.StartAirCooldown();
        SoundManager.instance.PlaySound(player.airSound);

        player.Stand();
        // Instantiate the air projectile
        if (gameObject != null)
        {
            currentAirProjectile = Object.Instantiate(gameObject, player.shootPoint.position, Quaternion.identity);
            SpriteRenderer fireballSprite = currentAirProjectile.GetComponent<SpriteRenderer>();
            fireballSprite.flipX = player.viewDirection < 0;

            Rigidbody2D airRb = currentAirProjectile.GetComponent<Rigidbody2D>();
            if (airRb != null)
            {
                airRb.linearVelocity = new Vector2(player.fireballSpeed * player.viewDirection, 0);
            }
        }

        // Immediately allow the player to move after casting the air spell
        //stateMachine.ChangeState(player.idleState); // Or player.moveState if appropriate
    }


    public override void Update()
    {
        base.Update();
        player.Stand();
        if (controlledLandObject != null)
        {
            // Allow the player to control the land object in all directions
            Vector2 moveInput = new Vector2(
                Input.GetAxisRaw("Horizontal"), // Get input for left/right movement
                Input.GetAxisRaw("Vertical")    // Get input for up/down movement
            );

            // Move the land object based on player input
            controlledLandObject.transform.Translate(moveInput * player.runSpeed * Time.deltaTime);

            // Drop the land object when a specific key is pressed (e.g., space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                returnPlayer();
            }
        }
    }

    public void ControlLand(GameObject landObject)
    {
        controlledLandObject = landObject;
    }

    public override void Exit()
    {
        base.Exit();
        if (currentAirProjectile != null)
        {
            Object.Destroy(currentAirProjectile); // Destroy the projectile when exiting the state
        }
    }

    public void returnPlayer()
    {
        controlledLandObject = null; // Stop controlling the object
        if (animTrigger)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

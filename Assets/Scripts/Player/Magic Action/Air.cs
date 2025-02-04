using TMPro;
using UnityEngine;

public class Air : PlayerState
{
    private GameObject currentAirProjectile;
    private GameObject controlledLandObject;
    // Reference to the TextMeshPro text object
    private TextMeshProUGUI spellText;
    public Air(Player player, StateMachine stateMachine, string animationName, GameObject gameObject, TextMeshProUGUI text)
        : base(player, stateMachine, animationName, gameObject)
    {
        spellText = text; // Initialize the TextMeshPro reference
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
        // Show the spell text
        if (spellText != null)
        {
            spellText.gameObject.SetActive(true);
        }
        if (controlledLandObject != null)
        {
            // Increase camera size when Air spell is cast
            player.ChangeCameraSize(7.5f); // Adjust this value based on preference
            // Get current player position
            Vector3 playerPosition = player.transform.position;
            // Allow the player to control the land object in all directions
            Vector2 moveInput = new Vector2(
                Input.GetAxisRaw("Horizontal"), // Get input for left/right movement
                Input.GetAxisRaw("Vertical")    // Get input for up/down movement
            );

            // Calculate the new position based on player input
            Vector3 newPosition = controlledLandObject.transform.position + (Vector3)(moveInput * player.runSpeed * Time.deltaTime);

            // Check if the new position is within the max distance from the player
            float distanceToPlayer = Vector3.Distance(playerPosition, newPosition);
            if (distanceToPlayer <= player.maxControlDistance)
            {
                // Allow movement only within the set range
                controlledLandObject.transform.position = newPosition;
            }

            // Update the line between the player and controlled land object
            player.UpdateLine(controlledLandObject.transform.position);

            // Drop the land object when a specific key is pressed (e.g., space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                returnPlayer();
            }
        }
        // Hide the spell text when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && spellText != null)
        {
            returnPlayer();
        }
    }

    public void ControlLand(GameObject landObject)
    {
        controlledLandObject = landObject;
    }

    public override void Exit()
    {
        base.Exit();

        // Reset the camera view when Air spell ends
        player.ResetCameraSize();

        if (currentAirProjectile != null)
        {
            Object.Destroy(currentAirProjectile); // Destroy the projectile when exiting the state
        }
        // Ensure the text is hidden when exiting the state
        if (spellText != null)
        {
            spellText.gameObject.SetActive(false);
        }
    }

    public void returnPlayer()
    {
        spellText.gameObject.SetActive(false);

        controlledLandObject = null; // Stop controlling the object

        player.HideLine(); // Hide the line when control ends

        if (animTrigger)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

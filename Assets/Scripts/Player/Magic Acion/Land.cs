using UnityEngine;

public class Land : PlayerState
{
    private GameObject currentLand; // Reference to the current land object
    public Land(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        if (!player.CanCastLand())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        base.Enter();

        // Start cooldown
        player.StartLandCooldown();

        // Check if a land object already exists
        if (currentLand != null)
        {
            Object.Destroy(currentLand); // Destroy the existing land object
        }

        // Instantiate a new land object at the player's position
        if (gameObject != null)
        {
            currentLand = Object.Instantiate(gameObject, player.shootPoint.position, Quaternion.identity);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (animTrigger)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

using UnityEngine;

public class Fireball : PlayerState
{
    public Fireball(Player player, StateMachine stateMachine, string animationName, GameObject gameObject) : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        if (!player.CanCastFireball())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        base.Enter();

        // Start cooldown
        player.StartFireballCooldown();
        SoundManager.instance.PlaySound(player.fireballSound);

        // Instantiate the fireball at the fire point
        GameObject fireballInstance = Object.Instantiate(gameObject, player.shootPoint.position, Quaternion.identity);

        // Set the fireball's velocity based on the player's facing direction
        Rigidbody2D fireballRb = fireballInstance.GetComponent<Rigidbody2D>();
        if (fireballRb != null)
        {
            fireballRb.linearVelocity = new Vector2(player.fireballSpeed * player.viewDirection, 0);
        }
        SpriteRenderer fireballSprite = fireballInstance.GetComponent<SpriteRenderer>();
        if (fireballSprite != null)
        {
            fireballSprite.flipX = player.viewDirection < 0;
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

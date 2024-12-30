using UnityEngine;

public class Water : PlayerState
{
    private GameObject currentShield;

    public Water(Player player, StateMachine stateMachine, string animationName, GameObject gameObject)
        : base(player, stateMachine, animationName, gameObject)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Instantiate the water shield and parent it to the player
        if (gameObject != null)
        {
            currentShield = Object.Instantiate(gameObject, player.transform.position, Quaternion.identity);
            currentShield.transform.SetParent(player.transform); // Make the shield follow the player
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

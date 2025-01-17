using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Player player => GetComponent<Player>();

    private void AnimationTriger()
    {
        player.AnimTrigger();
    }
}

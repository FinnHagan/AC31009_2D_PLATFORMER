using UnityEngine;

public class PlayerHitState : IPlayerState
{
    private PlayerStateManager stateManager;

    public PlayerHitState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Health>().HitTaken(stateManager.hitDamage); //Hit taken method handles how many lives the player has
        stateManager.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stateManager.enabled = false;
    }

    public void UpdateState()
    {

    }

    public void HandleInput()
    {

    }
}

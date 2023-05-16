using UnityEngine;

public class PlayerDieState : IPlayerState
{
    private PlayerStateManager stateManager;

    public PlayerDieState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().SetTrigger("die");
        stateManager.GetComponent<SoundEffects>().Play(stateManager.playerDeathSound);
        stateManager.GetComponent<Health>().dead = true;
        stateManager.enabled = false;
        stateManager.StartCoroutine(stateManager.respawner.RespawnTimer());
    }

    public void UpdateState()
    {
    }

    public void HandleInput()
    {

    }
}

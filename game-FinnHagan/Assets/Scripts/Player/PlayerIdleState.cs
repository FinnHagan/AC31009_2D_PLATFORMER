using UnityEngine;
public class PlayerIdleState : IPlayerState
{
    private PlayerStateManager stateManager;

    public PlayerIdleState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().SetBool("move", false);
    }

    public void UpdateState()
    {

    }

    public void HandleInput()
    {
        float xInput = Input.GetAxis("Horizontal"); //Getting the player's x coordinate
        if (Mathf.Abs(xInput) > 0)
        {
            stateManager.TransitionToState(typeof(PlayerMovingState));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && stateManager.OnGround())
        {
            stateManager.TransitionToState(typeof(PlayerJumpingState));
        }
    }

}
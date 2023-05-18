using UnityEngine;
public class PlayerJumpingState : IPlayerState
{
    private PlayerStateManager stateManager;

    public PlayerJumpingState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        // Perform state entry actions
        stateManager.Jump();
    }

    public void UpdateState()
    {
        float xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stateManager.OnGround())
            {
                stateManager.jumpsRemaining = stateManager.extraJumps - 1; // Reset jumps if grounded
                stateManager.Jump();
            }
            else if (stateManager.jumpsRemaining > 0)
            {
                stateManager.jumpsRemaining--;
                stateManager.Jump();
            }
        }
    }

    public void HandleInput()
    {
        float xInput = Input.GetAxis("Horizontal");

        // Transition to moving state if arrow keys are used mid air
        if (Mathf.Abs(xInput) > 0 && !stateManager.OnGround())
        {
            stateManager.TransitionToState(typeof(PlayerMovingState));
        }
    }
}

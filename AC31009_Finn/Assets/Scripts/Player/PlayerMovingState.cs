using UnityEngine;

public class PlayerMovingState : IPlayerState
{
    private PlayerStateManager stateManager;

    public PlayerMovingState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().SetBool("move", true);
    }

    public void UpdateState()
    {
       float xInput = Input.GetAxis("Horizontal");
        stateManager.GetComponent<Rigidbody2D>().velocity = new Vector2(xInput * stateManager.playerSpeed, stateManager.GetComponent<Rigidbody2D>().velocity.y);

        // Change character direction when turning left
        if (Mathf.Sign(xInput) != 0)
            stateManager.transform.localScale = new Vector3(7 * Mathf.Sign(xInput), 7, 7);

        stateManager.GetComponent<Animator>().SetBool("grounded", stateManager.OnGround());
    }

    public void HandleInput()
    {
        float xInput = Input.GetAxis("Horizontal");

        // Check for a single jump (not a double jump)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stateManager.OnGround())
            {
                stateManager.jumpsRemaining = stateManager.extraJumps - 1; // Reset jumps if grounded
                stateManager.TransitionToState(typeof(PlayerJumpingState)); //Otherwise check for double jump
            }
            else if (stateManager.jumpsRemaining > 0)
            {
                stateManager.jumpsRemaining = 0; // Only allow a single additional jump while moving
                stateManager.Jump();
            }
        }

        // Transition to idle state if there is no movement on the arrow keys
        if (Mathf.Abs(xInput) == 0)
        {
            stateManager.TransitionToState(typeof(PlayerIdleState));
        }
    }
}

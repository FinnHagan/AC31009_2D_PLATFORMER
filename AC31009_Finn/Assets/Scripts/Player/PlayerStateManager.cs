using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public float playerSpeed = 15f;
    public float jumpForce = 10f;
    public int extraJumps = 2;
    public int jumpsRemaining;
    public float hitDamage = 1f;
    public bool launched = false;
    private LayerMask groundLayer;
    public Respawner respawner;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;
    private LadderMovement climbing;
    public AudioClip jumpSound;
    public AudioClip playerDeathSound;

    public IPlayerState currentState { get; private set; }

    private Dictionary<Type, IPlayerState> availableStates = new Dictionary<Type, IPlayerState>();

    private void Awake()
    {
        availableStates.Add(typeof(PlayerIdleState), new PlayerIdleState(this));
        availableStates.Add(typeof(PlayerMovingState), new PlayerMovingState(this));
        availableStates.Add(typeof(PlayerJumpingState), new PlayerJumpingState(this));
        availableStates.Add(typeof(PlayerHitState), new PlayerHitState(this));
        availableStates.Add(typeof(PlayerDieState), new PlayerDieState(this));

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground"); // Assign the ground layer mask
        jumpsRemaining = extraJumps;
        climbing = GetComponent<LadderMovement>();
        respawner = GetComponent<Respawner>();
    }

    private void Start()
    {
        TransitionToState(typeof(PlayerIdleState));
    }

    public void TransitionToState(Type stateType)
    {
        if (!availableStates.ContainsKey(stateType))
        {
            Debug.LogError($"The state {stateType} is invalid");
            return;
        }

        if (currentState != null && currentState.GetType() == stateType)
        {
            Debug.LogWarning($"{stateType} is already active");
            return;
        }

        currentState = availableStates[stateType];
        currentState.EnterState();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
            currentState.HandleInput();
        }
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        SoundEffects.instance.Play(jumpSound);
        anim.SetTrigger("jump");
    }

    public bool OnGround()
    {
        Vector2 boxSize = new Vector2(bc.bounds.size.x - 0.1f, 0.05f);
        Collider2D hit = Physics2D.OverlapBox(bc.bounds.center - new Vector3(0, bc.bounds.extents.y, 0), boxSize, 0, groundLayer);
        if (hit != null)
            jumpsRemaining = extraJumps; // Reset jumps if grounded
        return hit != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("LaunchPad"))
        {
            launched = true;
        }
        else if (collision.collider.CompareTag("Ground"))
        {
            jumpsRemaining = extraJumps; //Resets jumps
            launched = false; //Allows player to jump once they've been grounded
        }
    }
}
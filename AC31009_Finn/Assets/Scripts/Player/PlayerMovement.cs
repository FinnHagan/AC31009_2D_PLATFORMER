using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 15f;
    public float jumpForce = 10f;
    private float deathHeight = -30f;
    private int extraJumps = 2;
    private int jumpsRemaining;
    private bool launched = false;


    private LayerMask groundLayer;


    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;
    private LadderMovement climbing;
    public AudioClip jumpSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.NameToLayer("Ground");
        jumpsRemaining = extraJumps;
        climbing = GetComponent<LadderMovement>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        FallDeath();
    }

    private void HandleMovementInput()
    {
        float xInput = Input.GetAxis("Horizontal"); //Getting the players X coordinate from Unity
        rb.velocity = new Vector2(xInput * playerSpeed, rb.velocity.y);

        // Change character direction when turning left
        if (Mathf.Sign(xInput) != 0)
            transform.localScale = new Vector3(7 * Mathf.Sign(xInput), 7, 7);

        anim.SetBool("move", !Mathf.Approximately(xInput, 0));
        anim.SetBool("grounded", OnGround());
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        SoundEffects.instance.Play(jumpSound);
        anim.SetTrigger("jump");
    }

    private void HandleJumpInput()
    {
        float xInput = Input.GetAxis("Horizontal");

        // Disable jumping when the player is on a ladder or used a launch pad
        if (climbing.onLadder || launched)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            if (OnGround())
                jumpsRemaining = extraJumps - 1; // Reset jumps if grounded
            else
                jumpsRemaining--;

            Jump();
        }
    }

    private void FallDeath()
    {
        Respawner respawner = GetComponent<Respawner>();
        if (transform.position.y < deathHeight)
        {
            StartCoroutine(respawner.RespawnTimer());
        }
    }

    private bool OnGround()
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

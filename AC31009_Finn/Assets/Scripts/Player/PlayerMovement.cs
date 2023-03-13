using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float deathHeight;


    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;
    private int jumpsRemaining;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        jumpsRemaining = extraJumps;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        UpdateAnimator();
        IsPlayerAlive();
    }

    private void HandleMovementInput()
    {
        float xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * playerSpeed, rb.velocity.y);

        // Change character direction when turning left
        if (Mathf.Sign(xInput) != 0)
            transform.localScale = new Vector3(7 * Mathf.Sign(xInput), 7, 7);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetTrigger("jump");
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            if (OnGround())
                jumpsRemaining = extraJumps - 1; // Reset jumps if grounded
            else
                jumpsRemaining--;

            Jump();
        }
    }

    private void UpdateAnimator()
    {
        float xInput = Input.GetAxis("Horizontal");
        anim.SetBool("move", !Mathf.Approximately(xInput, 0));
        anim.SetBool("grounded", OnGround());
    }

    private void IsPlayerAlive()
    {
        Health health = GetComponent<Health>();
        if (transform.position.y < deathHeight)
        {
            health.Respawn();
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
        if (collision.collider.CompareTag("Enemy"))
        {
            if (rb.velocity.y > 0) // if player is jumping
            {
                DefeatEnemy(collision.gameObject);
            }
            else
            {
                Health health = GetComponent<Health>();
                health.HitTaken(health.currentHealth);
            }
        }
    }

    private void DefeatEnemy(GameObject enemy)
    {
        enemy.GetComponent<BoxCollider2D>().enabled = false;
        enemy.GetComponent<EnemyMovement>().enabled = false;
    }

}

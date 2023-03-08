using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;
    [SerializeField] private float playerSpeed;
    [SerializeField] private LayerMask groundLayer;
    public float deathHeight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * playerSpeed, rb.velocity.y);

        //Change character direction when turning left
        if (Mathf.Sign(xInput) != 0)
            transform.localScale = new Vector3(Mathf.Sign(xInput), 1, 1);

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
            Jump();

        //Initialising animator values
        anim.SetBool("move", !Mathf.Approximately(xInput, 0));
        anim.SetBool("grounded", IsGrounded());

        // Check if player falls off the map
        if (transform.position.y < deathHeight)
        {
            Health health = GetComponent<Health>();
            health.HitTaken(health.currentHealth);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, playerSpeed);
        anim.SetTrigger("jump");
    }

    private bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(bc.bounds.size.x - 0.1f, 0.05f);
        Collider2D hit = Physics2D.OverlapBox(bc.bounds.center - new Vector3(0, bc.bounds.extents.y, 0), boxSize, 0, groundLayer);
        return hit != null;
    }
}

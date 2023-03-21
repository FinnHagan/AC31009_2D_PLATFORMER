using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float climbSpeed = 5f;
    private float vertical;
    private bool isClimbing;
    public bool onLadder; //Public so can be accessed by PlayerMovement script


    private void Awake()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if(onLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = 1f; //Restores gravity back to normal after coming off ladder
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            onLadder = false;
            isClimbing = false;
        }
    }
}

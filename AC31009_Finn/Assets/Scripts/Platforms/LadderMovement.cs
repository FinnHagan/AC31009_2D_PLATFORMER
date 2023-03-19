using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [SerializeField] private float climbSpeed;
    [SerializeField] private Rigidbody2D rb;
    private float vertical;
    private bool onLadder;
    private bool climbing;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if(onLadder && Mathf.Abs(vertical) > 0f)
        {
            climbing = true;
        }
    }

    private void FixedUpdate()
    {
        if(climbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = 1.5f;
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
            climbing = false;
        }
    }
}

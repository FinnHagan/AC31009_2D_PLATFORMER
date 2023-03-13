using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float hit;
    [SerializeField] private float moveLength;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float attackCooldown;

    private float leftLimit;
    private float rightLimit;
    private bool moveRight = true;
    private float lastAttackTime = 0f;

    private void Awake()
    {
        leftLimit = transform.position.x - moveLength;
        rightLimit = transform.position.x + moveLength;
    }

    private void Update()
    {
        HandleEnemyMovement();
    }

    private void HandleEnemyMovement()
    {
        float xInput = enemySpeed * Time.deltaTime;
        if (moveRight)
        {
            transform.position += new Vector3(xInput, 0, 0);
            if (transform.position.x >= rightLimit)
            {
                moveRight = false;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            transform.position -= new Vector3(xInput, 0, 0);
            if (transform.position.x <= leftLimit)
            {
                moveRight = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time - lastAttackTime >= attackCooldown)
        {
            Vector2 collisionDirection = collision.transform.position - transform.position;
            if (collisionDirection.y > 0 && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Defeated();
            }
            else
            {
                collision.GetComponent<Health>().HitTaken(hit);
                lastAttackTime = Time.time;
            }
        }
    }
    public void Defeated(float delay = 0.75f)
    {
        // Play death animation
        GetComponent<Animator>().SetTrigger("EnemyDeath");
        // Disable collider
        GetComponent<Collider2D>().enabled = false;
        // Disable movement
        enabled = false;
        // Destroy game object after a delay
        Destroy(gameObject, delay);
    }

}

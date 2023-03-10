using UnityEngine;
using UnityEngine.Windows;

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
        float xInput = enemySpeed * Time.deltaTime * (moveRight ? 1 : -1);
        transform.position = new Vector3(transform.position.x + xInput, transform.position.y, transform.position.z);
        // Change character direction when turning left
        if (Mathf.Sign(xInput) != 0)
            transform.localScale = new Vector3(10 * Mathf.Sign(xInput), 10, 10);

        if (transform.position.x >= rightLimit && moveRight)
        {
            moveRight = false;
        }
        else if (transform.position.x <= leftLimit && !moveRight)
        {
            moveRight = true;
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
    public void Defeated()
    {
        // Play death animation
        GetComponent<Animator>().SetTrigger("EnemyDeath");
        // Disable collider
        GetComponent<Collider2D>().enabled = false;
        // Disable movement
        enabled = false;
        // Destroy game object after a delay
        Destroy(gameObject, 0.75f);
    }
}

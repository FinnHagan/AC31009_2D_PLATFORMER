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
        float movement = enemySpeed * Time.deltaTime * (moveRight ? 1 : -1);
        transform.position = new Vector3(transform.position.x + movement, transform.position.y, transform.position.z);

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
            collision.GetComponent<Health>().HitTaken(hit);
            lastAttackTime = Time.time;
        }
    }
}

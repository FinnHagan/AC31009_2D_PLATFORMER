using UnityEngine;
public class EnemyMovingState : IEnemyState
{
    private EnemyStateManager stateManager;
    private float moveLength = 4f;
    private float enemySpeed = 4f;
    private float leftLimit;
    private float rightLimit;
    private bool moveRight = true;
    private float hit = 1f;
    private float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    public EnemyMovingState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;

        // Set initial left and right limits
        leftLimit = stateManager.transform.position.x - moveLength;
        rightLimit = stateManager.transform.position.x + moveLength;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().Play("EnemyMoving");
    }

    public void UpdateState()
    {
        float xInput = enemySpeed * Time.fixedDeltaTime;

        //Move the enemy to the right until it reaches its right limit, and then to to the left when it reaches its left limit
        if (moveRight)
        {
            stateManager.transform.position += new Vector3(xInput, 0, 0);
            if (stateManager.transform.position.x >= rightLimit)
            {
                moveRight = false;
                stateManager.transform.localScale = new Vector3(-stateManager.transform.localScale.x, stateManager.transform.localScale.y, stateManager.transform.localScale.z);
            }
        }
        else
        {
            stateManager.transform.position -= new Vector3(xInput, 0, 0);
            if (stateManager.transform.position.x <= leftLimit)
            {
                moveRight = true;
                stateManager.transform.localScale = new Vector3(-stateManager.transform.localScale.x, stateManager.transform.localScale.y, stateManager.transform.localScale.z);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if player is vulnerable to being hit based on enemy attack cooldown
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Vector2 collisionDirection = collision.transform.position - stateManager.transform.position;
                if (collisionDirection.y > 0 && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    // Player stomped on enemy's head
                    stateManager.TransitionToState(typeof(EnemyDeathState));
                }
                else
                {
                    //Take health of player as enemy hit them
                    collision.GetComponent<Health>().HitTaken(hit);
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}

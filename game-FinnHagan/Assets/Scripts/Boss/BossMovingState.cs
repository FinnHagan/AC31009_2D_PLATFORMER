using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BossMovingState : IBossState
{
    private BossStateManager stateManager;
    private float moveLength = 8f;
    private float bossSpeed = 4f;
    private float leftLimit;
    private float rightLimit;
    private bool moveRight = true;

    public BossMovingState(BossStateManager stateManager)
    {
        this.stateManager = stateManager;

        // Set initial left and right limits
        leftLimit = stateManager.transform.position.x - moveLength;
        rightLimit = stateManager.transform.position.x + moveLength;
    }

    public void EnterState()
    {

    }

    public void UpdateState()
    {
        float xInput = bossSpeed * Time.deltaTime;

        //Move the boss to the right until it reaches its right limit, and then to to the left when it reaches its left limit
        if (moveRight)
        {
            stateManager.transform.position += new Vector3(xInput, 0, 0);
            if (stateManager.transform.position.x >= rightLimit)
            {
                moveRight = false;
                stateManager.transform.localScale = new Vector3(-stateManager.transform.localScale.x, stateManager.transform.localScale.y, stateManager.transform.localScale.z);
                stateManager.GetComponent<Animator>().SetTrigger("bossMove");
            }
        }
        else
        {
            stateManager.transform.position -= new Vector3(xInput, 0, 0);
            if (stateManager.transform.position.x <= leftLimit)
            {
                moveRight = true;
                stateManager.transform.localScale = new Vector3(-stateManager.transform.localScale.x, stateManager.transform.localScale.y, stateManager.transform.localScale.z);
                stateManager.GetComponent<Animator>().SetTrigger("bossMove");
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the player is vulnerable to being hit based off of the enemy attack cooldown
        if (collision.CompareTag("Player") && Time.time - stateManager.lastAttackTime >= stateManager.attackCooldown)
        {
            Vector2 collisionDirection = collision.transform.position - stateManager.transform.position;
            if (collisionDirection.y > 0 && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                stateManager.hitCount++;
                //Boss has 3 health so if he is hit 3 times he dies
                if (stateManager.hitCount == 3)
                {
                    stateManager.TransitionToState(typeof(BossDeathState));
                }
                else
                {
                    stateManager.TransitionToState(typeof(BossHitState));
                }

            }
            //Take health from player as they have been hit
            else
            {
                collision.GetComponent<Health>().HitTaken(stateManager.hit);
                stateManager.lastAttackTime = Time.time;
            }
        }
    }
}

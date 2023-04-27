using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private float hit = 1f;
    private float moveLength = 10f;
    private float bossSpeed = 10f;
    private float attackCooldown = 1f;
    private float leftLimit;
    private float rightLimit;
    private bool moveRight = true;
    private float lastAttackTime = 0f;
    private int hitCount = 0;
    public AudioClip bossHitSound;
    public AudioClip bossDeathSound;

    private void Awake()
    {
        //Handles the distance the enemy can move left and right
        leftLimit = transform.position.x - moveLength;
        rightLimit = transform.position.x + moveLength;
    }

    private void Update()
    {
        float xInput = bossSpeed * Time.deltaTime;

        //Move the enemy to the right until it reaches its right limit, and then to to the left when it reaches its left limit
        if (moveRight)
        {
            transform.position += new Vector3(xInput, 0, 0);
            if (transform.position.x >= rightLimit)
            {
                moveRight = false;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                GetComponent<Animator>().SetTrigger("bossMove");
            }
        }
        else
        {
            transform.position -= new Vector3(xInput, 0, 0);
            if (transform.position.x <= leftLimit)
            {
                moveRight = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                GetComponent<Animator>().SetTrigger("bossMove");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the player is vulnerable to being hit based off of the enemy attack cooldown
        if (collision.CompareTag("Player") && Time.time - lastAttackTime >= attackCooldown)
        {
            Vector2 collisionDirection = collision.transform.position - transform.position;
            if (collisionDirection.y > 0 && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                hitCount++;
                if (hitCount == 3)
                {
                    GetComponent<Animator>().SetTrigger("bossDie");
                    Defeated();
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("bossHit");
                    SoundEffects.instance.Play(bossHitSound);
                    lastAttackTime = Time.time;
                }

            }
            else
            {
                collision.GetComponent<Health>().HitTaken(hit);
                lastAttackTime = Time.time;
            }
        }
    }

    public void Defeated(float delay = 3f)
    {
        SoundEffects.instance.Play(bossDeathSound);
        GetComponent<Collider2D>().enabled = false;
        enabled = false; //Disables movement
        Destroy(gameObject, delay); //Destroys the enemy object after a delay the length of the animation
    }

}

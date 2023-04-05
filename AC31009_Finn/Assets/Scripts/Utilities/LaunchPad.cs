using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    private float bounce = 22f;
    private float freezeTime = 1f;
    private Animator anim;
    private float timer;
    private float animSpeed; //When the animation gets frozen it messes up the animation speed, so this is necessary to keep the animation playing after freezing

    private void Start()
    {
        anim = GetComponent<Animator>();
        animSpeed = anim.speed; // save the original animation speed
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                anim.Play("LaunchPlayer");
                anim.speed = animSpeed; // reset the animation speed
            }
            else if (timer > 0f)
            {
                anim.speed = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            timer = freezeTime;
        }
    }
}

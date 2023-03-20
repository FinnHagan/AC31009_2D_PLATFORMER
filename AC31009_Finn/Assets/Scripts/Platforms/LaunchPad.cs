using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] private float bounce;
    [SerializeField] private float freezeTime;
    private Animator anim;
    private float timer;
    private float originalAnimSpeed; // add this new variable

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalAnimSpeed = anim.speed; // save the original animation speed
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                anim.Play("LaunchPlayer");
                anim.speed = originalAnimSpeed; // reset the animation speed
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

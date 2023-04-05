using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float currentHealth { get; private set; }
    private float maxHealth = 3f;
    private Animator anim;
    private bool dead;

    public void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void HitTaken(float hit)
    {
        currentHealth = Mathf.Clamp(currentHealth - hit, 0, maxHealth); //Takes one hit point from the total hearts the player has at that time

        if (currentHealth > 0)
        {
            anim.SetTrigger("hit");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                OnDeath();

                // Start timer to respawn the player after 3 seconds
                StartCoroutine(RespawnTimer());
            }
        }
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(2f); // Waits for a bit before respawning the player
        RespawnPlayer();
    }


    public void RestoreHeart(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void OnDeath()
    {
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
    }

    public void RespawnPlayer()
    {
        // Reset position to a predefined spawn point
        transform.position = new Vector3(-16.9f, -0.69f, 0);

        // Reset health and any other necessary variables
        currentHealth = maxHealth;
        anim.ResetTrigger("die");
        anim.Play("Idle");

        // Reactivate any disabled components
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Set the dead variable to false
        dead = false;
    }
}

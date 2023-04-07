using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; set; }
    public float maxHealth = 3f;
    private Animator anim;
    private bool dead;
    private Respawner respawner;
    public bool checkInvincibility { get; set; } // Add isInvincible variable

    public void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        respawner = GetComponent<Respawner>();
    }

    public void HitTaken(float hit)
    {
        if (!checkInvincibility)
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
                    StartCoroutine(respawner.RespawnTimer());
                    dead = false;
                }
            }
        }
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
}

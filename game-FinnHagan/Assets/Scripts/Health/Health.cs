using UnityEngine;
public class Health : MonoBehaviour
{
    public float currentHealth { get; set; }
    public float maxHealth = 3f;
    private Animator anim;
    public bool dead;
    private Respawner respawner;
    public bool checkInvincibility { get; set; }
    public AudioClip hitSound;
    public AudioClip playerDeathSound;
    private float deathHeight = -50f;
    private bool isFalling;

    public void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        respawner = GetComponent<Respawner>();
    }

    private void Update()
    {
        FallDeath();
    }

    public void HitTaken(float hit)
    {
        if (!checkInvincibility) //Checks if the player has collected the invincibility power-up
        {
            currentHealth = Mathf.Clamp(currentHealth - hit, 0, maxHealth); //Calculates there health based off of how much health they have and how many hits they've taken

            if (currentHealth > 0)
            {
                anim.SetTrigger("hit");
                SoundEffects.instance.Play(hitSound);
            }
            else
            {
                if (!dead)
                {
                    anim.SetTrigger("die");
                    SoundEffects.instance.Play(playerDeathSound);
                    dead = true;
                    StartCoroutine(respawner.RespawnTimer());
                }
            }
        }
    }

    private void FallDeath()
    {
        if (transform.position.y < deathHeight && !isFalling) //Checks if the player is above death height and if they are falling
        {
            anim.SetTrigger("die");
            SoundEffects.instance.Play(playerDeathSound);
            StartCoroutine(respawner.RespawnTimer());
            isFalling = true;
        }
    }

    public void RestoreHeart(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}

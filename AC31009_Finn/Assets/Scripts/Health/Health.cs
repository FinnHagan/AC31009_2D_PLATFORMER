using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; set; }
    public float maxHealth = 3f;
    private Animator anim;
    private bool dead;
    private Respawner respawner;
    public bool checkInvincibility { get; set; }
    public AudioClip hitSound;
    public AudioClip playerDeathSound;



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
                    dead = false;
                }
            }
        }
    }

    public void RestoreHeart(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

}

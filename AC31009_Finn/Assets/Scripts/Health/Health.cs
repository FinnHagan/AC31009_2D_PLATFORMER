using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    public void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
    }

    public void HitTaken(float hit)
    {
        currentHealth = Mathf.Clamp(currentHealth - hit, 0, startHealth);

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
            }
        }
    }


    public void RestoreHeart(float val)
    {
        currentHealth = Mathf.Clamp(currentHealth + val, 0, startHealth);
    }

    public void OnDeath()
    {
        anim.SetTrigger("die");
        GetComponent<PlayerMovement>().enabled = false;
        dead = true;
    }
}

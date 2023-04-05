using UnityEngine;
using System.Collections;


public class Respawner : MonoBehaviour
{
    private Animator anim;
    private Health health;
    private GameController gameController;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        gameController = FindObjectOfType<GameController>();

    }

    public void Respawn()
    {
        // Reset position to a predefined spawn point
        transform.position = new Vector3(-16.9f, -0.69f, 0);

        // Reset health and any other necessary variables
        health.currentHealth = health.maxHealth;
        anim.ResetTrigger("die");
        anim.Play("Idle");

        // Reactivate any disabled components
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        gameController.RestartLevel1();
    }


    public IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(2f); // Waits for a bit before respawning the player
        Respawn();
    }
}

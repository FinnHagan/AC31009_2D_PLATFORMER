using UnityEngine;
public class EnemyDeathState : IEnemyState
{
    private EnemyStateManager stateManager;
    public EnemyDeathState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public void EnterState()
    {
        stateManager.GetComponent<Animator>().SetTrigger("EnemyDeath");
        SoundEffects.instance.Play(stateManager.enemyDeathSound);

        // Disable collider and movement
        stateManager.gameObject.GetComponent<Collider2D>().enabled = false;
        stateManager.enabled = false;

        // Destroy the enemy after a short delay (death animation length)
        GameObject.Destroy(stateManager.gameObject, 0.75f);
    }

    public void UpdateState()
    {
        // Enemy is dead so no update
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle trigger enter events here
    }
}

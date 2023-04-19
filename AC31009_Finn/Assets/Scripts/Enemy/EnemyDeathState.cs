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
        SoundEffects.instance.Play(stateManager.GetComponent<EnemyController>().enemyDeathSound);

        // Disable collider and movement
        stateManager.gameObject.GetComponent<Collider2D>().enabled = false;
        stateManager.gameObject.GetComponent<EnemyController>().enabled = false;

        // Destroy the enemy after a short delay (death animation length)
        GameObject.Destroy(stateManager.gameObject, 0.75f);
    }

    public void UpdateState()
    {
        // Enemy is dead so no update
    }

    public void ExitState()
    {
        // when player dies and enemies respawn
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // handle trigger enter events here
    }
}

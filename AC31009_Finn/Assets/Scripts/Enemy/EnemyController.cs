using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AudioClip enemyDeathSound;
    EnemyStateManager stateManager;

    public void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }

    private void Update()
    {
        if (stateManager.currentState != null)
        {
            stateManager.currentState.UpdateState();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stateManager.currentState == null) return;
        stateManager.currentState.OnTriggerEnter2D(collision);
    }
}

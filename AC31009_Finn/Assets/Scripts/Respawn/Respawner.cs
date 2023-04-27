using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour
{
    public DeathScreen deathScreen;

    public void GameOver()
    {
        deathScreen.OnDeath();
    }

    public IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(1.5f);
        GameOver();
    }
}

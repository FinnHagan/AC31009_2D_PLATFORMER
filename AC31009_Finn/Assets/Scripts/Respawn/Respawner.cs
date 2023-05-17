using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
    public DeathScreen deathScreen;

    public void GameOver()
    {
        deathScreen.OnDeath();
    }

    public IEnumerator RespawnTimer()
    {
        //Want there to be a  1.5 second delay so that the player death animation can be seen before the death screen.
        yield return new WaitForSeconds(1.5f);
        GameOver();
    }
}

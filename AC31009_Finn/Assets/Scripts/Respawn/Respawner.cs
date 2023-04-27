using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Respawner : MonoBehaviour
{
    public AudioClip playerDeathSound;

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public IEnumerator RespawnTimer()
    {
        SoundEffects.instance.Play(playerDeathSound);
        yield return new WaitForSeconds(3f); // Waits for a bit before respawning the player
        Respawn();
    }
}

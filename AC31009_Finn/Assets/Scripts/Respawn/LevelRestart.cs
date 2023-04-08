using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    public void RestartLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


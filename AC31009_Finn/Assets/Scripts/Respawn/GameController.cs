using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void RestartLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Health playerHealth;
    private Image totalHearts;
    private Image currentHearts;

    private void Awake()
    {
        playerHealth = FindObjectOfType<Health>();
        totalHearts = GameObject.Find("TotalHearts").GetComponent<Image>();
        currentHearts = GameObject.Find("CurrentHearts").GetComponent<Image>();
    }

    public void Start()
    {
        totalHearts.fillAmount = playerHealth.currentHealth / 4; //Makes the health bar fill up with 3 red hearts
    }

    public void Update() 
    {
        currentHearts.fillAmount = playerHealth.currentHealth / 4; //Every time the player takes a hit, one heart gets blacked out
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHearts;
    [SerializeField] private Image currentHearts;

    public void Start()
    {
        totalHearts.fillAmount = playerHealth.currentHealth / 4;
    }

    public void Update() 
    {
        currentHearts.fillAmount = playerHealth.currentHealth / 4;
    }
}

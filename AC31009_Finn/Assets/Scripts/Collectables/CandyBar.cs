using UnityEngine;
using UnityEngine.UI;

public class CandyBar : MonoBehaviour
{
    [SerializeField] private Candy playerCandy;
    [SerializeField] private Image totalCandy;
    [SerializeField] private Image currentCandy;

    public void Start()
    {
        totalCandy.fillAmount = 0.75f;
    }


    public void Update()
    {
        currentCandy.fillAmount = playerCandy.currentCandy / 4;
    }
}

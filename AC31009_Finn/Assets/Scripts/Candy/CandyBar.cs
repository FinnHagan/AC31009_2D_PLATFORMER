using UnityEngine;
using UnityEngine.UI;

public class CandyBar : MonoBehaviour
{
    private Candy playerCandy;
    private Image totalCandy;
    private Image currentCandy;


    private void Awake()
    {
        playerCandy = FindObjectOfType<Candy>();
        totalCandy = GameObject.Find("TotalCandy").GetComponent<Image>();
        currentCandy = GameObject.Find("CurrentCandy").GetComponent<Image>();

    }

    public void Start()
    {
        totalCandy.fillAmount = 0.75f; //Fill the candy bar UI with black so it shows the items need collected
    }


    public void Update()
    {
        currentCandy.fillAmount = playerCandy.currentCandy / 4; //Fill the black bar with colour each time a candy item is collected
    }
}

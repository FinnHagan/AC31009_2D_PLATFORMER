using UnityEngine;

public class Candy : MonoBehaviour
{
    private float maxCandy = 3f;
    public float currentCandy { get; private set; }

    public void Awake()
    {
        currentCandy = 0f; //none collected at the start
    }

    public void CollectCandy(float amount)
    {
        currentCandy = Mathf.Clamp(currentCandy + amount, 0, maxCandy);
    }
}

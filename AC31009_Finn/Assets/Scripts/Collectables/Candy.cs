using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour
{
    [SerializeField] private float maxCandy;
    public float currentCandy { get; private set; }

    public void Awake()
    {
        currentCandy = 0f; // set initial candy to 0
    }

    public void CollectCandy(float amount)
    {
        currentCandy = Mathf.Clamp(currentCandy + amount, 0, maxCandy);
    }
}

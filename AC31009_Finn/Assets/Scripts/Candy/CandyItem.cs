using UnityEngine;

public class CandyItem : MonoBehaviour
{
    private float candyValue = 1f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Candy>().CollectCandy(candyValue);
            gameObject.SetActive(false);
        }
    }
}

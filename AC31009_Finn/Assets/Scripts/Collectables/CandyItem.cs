using UnityEngine;

public class CandyItem : MonoBehaviour
{
    [SerializeField] private float candy;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Candy>().CollectCandy(candy);
            gameObject.SetActive(false);
        }
    }
}

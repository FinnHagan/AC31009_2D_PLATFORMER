using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [SerializeField] private float heartValue;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.GetComponent<Health>().RestoreHeart(heartValue);
            gameObject.SetActive(false);
        }
    }
}

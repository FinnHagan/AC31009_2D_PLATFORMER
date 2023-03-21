using UnityEngine;

public class HeartItem : MonoBehaviour
{
    private float heartValue = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().RestoreHeart(heartValue);
            gameObject.SetActive(false);
        }
    }
}

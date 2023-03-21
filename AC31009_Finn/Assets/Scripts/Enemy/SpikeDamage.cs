using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private float hit = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().HitTaken(hit);
    }

}

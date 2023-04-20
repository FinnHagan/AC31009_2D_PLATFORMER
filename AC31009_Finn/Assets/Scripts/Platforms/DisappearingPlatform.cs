using System.Collections;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private float dissapearTime = 0.01f;
    private float destroyTime = 4f;
    private Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(dissapearTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyTime);
    }

}



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
        rb.bodyType = RigidbodyType2D.Dynamic; //Created a separate prefab for dissapearing tiles and changed rigid body type to dynamic so that it could fall down and thus be destroyed
        Destroy(gameObject, destroyTime);
    }

}



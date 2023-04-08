using UnityEngine;

public class CandyItem : MonoBehaviour
{
    private float candyValue = 1f;
    public AudioClip candySound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Candy>().CollectCandy(candyValue);
            SoundEffects.instance.Play(candySound);
            gameObject.SetActive(false);
        }
    }
}

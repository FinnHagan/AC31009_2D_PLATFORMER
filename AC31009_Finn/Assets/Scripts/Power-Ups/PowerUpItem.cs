using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public AudioClip powerUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PowerUpController>().ActivatePowerup();
            SoundEffects.instance.Play(powerUpSound);
            gameObject.SetActive(false);
        }
    }

}

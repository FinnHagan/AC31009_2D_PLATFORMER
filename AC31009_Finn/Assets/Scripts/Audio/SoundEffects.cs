using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance { get; private set; }
    private AudioSource audioSource; 


    public void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
            audioSource.PlayOneShot(clip);
    }

}

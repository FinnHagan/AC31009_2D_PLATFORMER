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
    //ALlows for the use of a singleton design pattern throughout  all the codebase
    public void Play(AudioClip clip)
    {
            audioSource.PlayOneShot(clip);
    }

}

using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Transform prevPlatform;
    [SerializeField] private Transform nextPlatform;
    [SerializeField] private CameraControl camCon;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            camCon.MoveToPlatform(collider.transform.position.x < transform.position.x ? nextPlatform : prevPlatform);
        }
    }
}

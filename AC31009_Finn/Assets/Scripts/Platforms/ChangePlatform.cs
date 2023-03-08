using UnityEngine;

public class ChangePlatform : MonoBehaviour
{
    [SerializeField] private Transform prevPlatform;
    [SerializeField] private Transform nextPlatform;
    [SerializeField] private CameraControl cameraControl;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            cameraControl.MoveToPlatform(collider.transform.position.x < transform.position.x ? nextPlatform : prevPlatform);
        }
    }
}

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float distanceForward;
    [SerializeField] private float camSpeed;

    private float checkForward;

    private void Update()
    {
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        checkForward = Mathf.Lerp(checkForward, (distanceForward * Player.localScale.x), Time.deltaTime * camSpeed);
    }

    public void MoveToPlatform(Transform newPlatform)
    {
        transform.position = new Vector3(newPlatform.position.x, transform.position.y, transform.position.z);
    }
}

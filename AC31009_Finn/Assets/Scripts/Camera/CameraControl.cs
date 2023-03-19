using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float distanceForward;
    [SerializeField] private float camSpeed;

    private float checkForward;
    private float lastPlayerY;

    private void Update()
    {
        // Update horizontal camera position
        transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);

        // Calculate target camera distance
        float targetDistance = distanceForward * Player.localScale.x;

        // Interpolate towards the target camera distance
        checkForward = Mathf.Lerp(checkForward, targetDistance, Time.deltaTime * camSpeed);

        // Update vertical camera position
        float playerHeight = Player.position.y;
        float cameraHeight = transform.position.y;

        // Check if player has fallen below camera height
        if (playerHeight < cameraHeight)
        {
            // Reset camera height to player height
            transform.position = new Vector3(transform.position.x, playerHeight, transform.position.z);
        }
        else
        {
            // Update camera height
            float distanceUp = playerHeight - lastPlayerY;
            transform.position += Vector3.up * distanceUp;
        }

        // Store player's last height
        lastPlayerY = playerHeight;
    }

    public void MoveToPlatform(Transform newPlatform)
    {
        transform.position = new Vector3(newPlatform.position.x, transform.position.y, transform.position.z);
    }


}

using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    private float platformSpeed = 10f;
    private float platformDistance = 0.05f;
    private Transform targetWaypoint;

    private void Start()
    {
        targetWaypoint = waypoint2;

        StartCoroutine(MoveToNextWaypoint());
    }

    //There are two way points for each platform in Unity of which the tile will move between and essentially 'bounce' off if you will, going to and fro the points repeatedly
    private IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            Vector3 targetPosition = targetWaypoint.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, platformSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < platformDistance)
            {
                // Switch the target waypoint
                if (targetWaypoint == waypoint1)
                {
                    targetWaypoint = waypoint2;
                }
                else
                {
                    targetWaypoint = waypoint1;
                }
            }

            yield return null;
        }
    }
}

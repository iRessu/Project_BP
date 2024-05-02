using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButtonHandler : MonoBehaviour
{
    public Moving_Platform platform;
    public float delayTime = 2f;
    private bool isMoving = false;
    private Coroutine movementCoroutine;
    public int waitAtWaypointIndex = 1;


    public void StartPlatform()
    {
        platform.StartMoving();
    }

    public void StopPlatform()
    {
        platform.StopMoving();
    }

    public void TogglePlatformMovement()
    {
        if(!isMoving)
        {
            movementCoroutine = StartCoroutine(MovePlatform());
            isMoving = true;
        }
        else
        {
            StopCoroutine(movementCoroutine);
            platform.StopMoving();
            platform.SetWaypointIndex(0);
            isMoving = false;
        }
    }


    private IEnumerator MovePlatform()
    {
        platform.StartMoving();

        while(Vector2.Distance(platform.transform.position, platform._waypoints[1].position) > platform._checkDistance)
        {
            yield return null;
        }

        platform.StopMoving();

        yield return new WaitForSeconds(delayTime);

        platform.StartMoving();
        platform.SetWaypointIndex(0);

        while (Vector2.Distance(platform.transform.position, platform._waypoints[0].position) > platform._checkDistance)
        {
            yield return null;
        }

        platform.StopMoving();
        isMoving = false;
    }

    private IEnumerator MoveToNextWaypoint()
    {
        int currentWaypointIndex = platform.CurrentWaypointIndex;
        int nextWaypointIndex = (currentWaypointIndex + 1) % platform._waypoints.Length;

        platform.SetWaypointIndex(currentWaypointIndex);

        yield return new WaitUntil(() => Vector2.Distance(platform.transform.position,
            platform._waypoints[nextWaypointIndex].position) < platform._checkDistance);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButtonHandler : MonoBehaviour
{
    public Moving_Platform platform;
    public float delayTime = 2f;
    private bool canMove = true;
    private bool movingForward = true;
    private Coroutine movementCoroutine;
    public int waitAtWaypointIndex = 1;


    public void MovePlatform()
    {
        platform.StartMoving();
    }

    public void StopPlatform()
    {
        platform.StopMoving();
    }

    public void ActivatePlatform()
    {
        if(canMove)
        {
            canMove = false;
                if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(StartPlatform());
        }
    }


    public IEnumerator StartPlatform()
    {
        platform.StartMoving();

        int currentWaypointIndex = platform.CurrentWaypointIndex;

        while (true)
        {
            if(movingForward)
            {
                currentWaypointIndex++;
                if(currentWaypointIndex >= platform._waypoints.Length)
                {
                    currentWaypointIndex = platform._waypoints.Length - 1;
                    movingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if(currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 0;
                    movingForward = true;
                }
            }
            platform.SetWaypointIndex(currentWaypointIndex);
            yield return new WaitUntil(() => Vector2.Distance(platform.transform.position,
                platform._waypoints[currentWaypointIndex].position) < platform._checkDistance);

            if ((movingForward && currentWaypointIndex == waitAtWaypointIndex) ||
                (!movingForward && currentWaypointIndex == 0))
            {
                platform.StopMoving();
                yield return new WaitUntil(() => canMove);
                platform.StartMoving();
            }
        }

    }

}

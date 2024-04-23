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
            StartCoroutine(StartPlatform());
        }
    }


    public IEnumerator StartPlatform()
    {
        platform.StartMoving();

        while(true)
        {
            if(movingForward)
            {
                if (platform.CurrentWaypointIndex < platform._waypoints.Length - 1)
                {
                    platform.SetWaypointIndex(platform.CurrentWaypointIndex + 1);
                    yield return new WaitUntil(() => Vector2.Distance(platform.transform.position,
                        platform._waypoints[platform.CurrentWaypointIndex].position) < platform._checkDistance);
                }
                else
                {
                    movingForward = false;
                    platform.StopMoving();
                    yield return new WaitForSeconds(delayTime);
                    platform.StartMoving();
                }
            } 
            else
            {
                if (platform.CurrentWaypointIndex > 0)
                {
                    platform.SetWaypointIndex(platform.CurrentWaypointIndex - 1);
                    yield return new WaitUntil(() => Vector2.Distance(platform.transform.position,
                        platform._waypoints[platform.CurrentWaypointIndex].position) < platform._checkDistance);
                }
                else
                {
                    movingForward = true;
                    platform.StopMoving();
                    yield return new WaitForSeconds(delayTime);
                    platform.StartMoving();
                }

            }

        }
    }

}

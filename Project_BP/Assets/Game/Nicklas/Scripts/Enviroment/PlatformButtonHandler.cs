using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButtonHandler : MonoBehaviour
{
    public Moving_Platform platform;
    public float delayTime = 2f;
    private bool canMove = true;
    private bool shouldWait = false;
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

        while(platform._currentWaypointIndex < platform._waypoints.Length)
        {
           if(platform._currentWaypointIndex == waitAtWaypointIndex)
            {
                shouldWait = true;
                yield return new WaitForSeconds(delayTime);
                shouldWait = false;
            }
            yield return new WaitUntil(() => Vector2.Distance(platform.transform.position, platform._waypoints[platform._currentWaypointIndex].position) < platform._checkDistance);
            platform._currentWaypointIndex++;
        }

        platform._currentWaypointIndex = 0;
        yield return new WaitUntil(() => Vector2.Distance(platform.transform.position, platform._waypoints[0].position) < platform._checkDistance);
        platform.StopMoving();

        canMove = true;
    }
}

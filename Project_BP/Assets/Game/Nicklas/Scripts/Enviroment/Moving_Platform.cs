using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance = 0.05f;

    private Transform _targetWaypoint;
    private int _currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _targetWaypoint = _waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            _targetWaypoint.position,
            _speed * Time.deltaTime);

        float distanceToWaypoint = Vector2.Distance(transform.position, _targetWaypoint.position);
        Debug.Log("Distance to waypoint: " + distanceToWaypoint);
        if(Vector2.Distance(transform.position, _targetWaypoint.position) < _checkDistance)
        {
            _targetWaypoint = GetNextWaypoint();
        }
    }



    private Transform GetNextWaypoint()
    {
        _currentWaypointIndex++;
        if(_currentWaypointIndex >= _waypoints.Length )
        {
            _currentWaypointIndex = 0;
        }

        return _waypoints[_currentWaypointIndex];
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var playerMovement = other.collider.GetComponent<Movement_Test>();
        var backpack = other.collider.GetComponent<Backpack_Script>();
        if(playerMovement != null || backpack != null)
        {
            playerMovement.SetParent(transform);
            backpack.BackPackSetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var playerMovement = other.collider.GetComponent<Movement_Test>();
        var backpack = other.collider.GetComponent<Backpack_Script>();
        if(playerMovement != null || backpack != null)
        {
            playerMovement.ResetParent();
            backpack.BackPackResetParent();
        }
    }
}

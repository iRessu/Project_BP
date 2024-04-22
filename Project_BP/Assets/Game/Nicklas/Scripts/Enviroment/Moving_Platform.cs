using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] public Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] public float _checkDistance = 0.05f;
    [NonSerialized] public int _currentWaypointIndex = 0;


    private Transform _targetWaypoint;
    public bool isMoving;
   

    // Start is called before the first frame update
    void Start()
    {
        _targetWaypoint = _waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.position = Vector2.MoveTowards(
                   transform.position,
                   _targetWaypoint.position,
                   _speed * Time.deltaTime);
        }
   

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
        if(playerMovement != null && other.collider.CompareTag("Player"))
        {
            playerMovement.SetParent(transform);        
        }

        if (backpack != null && other.collider.CompareTag("Backpack"))
        {
            backpack.BackPackSetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var playerMovement = other.collider.GetComponent<Movement_Test>();
        var backpack = other.collider.GetComponent<Backpack_Script>();
        if(playerMovement != null && other.collider.CompareTag("Player"))
        {
            playerMovement.ResetParent();
        }
        if (backpack != null && other.collider.CompareTag("Backpack"))
        {
            backpack.BackPackResetParent();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}

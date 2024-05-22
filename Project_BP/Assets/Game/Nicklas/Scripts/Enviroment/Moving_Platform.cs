using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] public Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] public float _checkDistance = 0.05f;


 
    private int _currentWaypointIndex = 0;
    private Transform _targetWaypoint;
    public bool isMoving;

    private Vector3 _previousPosition;
    private Vector3 _movementDelta;
    private bool _playerOnPlatform;
   

    // Start is called before the first frame update
    void Start()
    {
        _targetWaypoint = _waypoints[0];
        _previousPosition = transform.position;
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

        _movementDelta = transform.position - _previousPosition;
        _previousPosition = transform.position;
   

        if(Vector2.Distance(transform.position, _targetWaypoint.position) < _checkDistance)
        {
            _currentWaypointIndex =(_currentWaypointIndex +1) % _waypoints.Length;
            _targetWaypoint= _waypoints[_currentWaypointIndex];
        }
    }

    private void FixedUpdate()
    {
        if(_playerOnPlatform)
        {
            var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            
            if(playerController != null)
            {
                playerController.transform.position += _movementDelta;
            }
        }
    }


    public void SetWaypointIndex(int index)
    {
        _currentWaypointIndex = index;
        _targetWaypoint = _waypoints[_currentWaypointIndex];
    }

  /* private Transform GetNextWaypoint()
    {
        _currentWaypointIndex++;
        if(_currentWaypointIndex >= _waypoints.Length )
        {
            _currentWaypointIndex = 0;
        }

        return _waypoints[_currentWaypointIndex];
    }*/


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
        {
            if(other.collider.CompareTag("Player"))
            {
                _playerOnPlatform = true;
            }
        }
       
        var backpack = other.collider.GetComponent<Backpack_Script>();
        
      

        if (backpack != null && other.collider.CompareTag("Backpack"))
        {
            backpack.BackPackSetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MovingPlatform"))
        {
            if(other.collider.CompareTag("Player"))
            {
                _playerOnPlatform = false;
            }
        }
       
        var backpack = other.collider.GetComponent<Backpack_Script>();
       
      
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

    public int CurrentWaypointIndex => _currentWaypointIndex;
}

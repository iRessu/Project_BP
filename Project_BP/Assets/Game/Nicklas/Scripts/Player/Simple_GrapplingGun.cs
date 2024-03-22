using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_GrapplingGun : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _linerenderer;
    public Rigidbody2D _rigidbody;
    Player_Movement playerMovement;

    public float pullSpeed = 5f;
    public LayerMask grappleLayer;
    private Vector2 grapplePoint;
    private bool isGrappling = false;
    private float distanceToGrapplePoint;

    // Start is called before the first frame update
    void Start()
    {
         playerMovement = FindObjectOfType<Player_Movement>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartGrapple();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopGrapple();
        }
        if(isGrappling)
        {
            PullPlayer();
        }
    }

    void StartGrapple()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - (Vector2)transform.position, Mathf.Infinity, grappleLayer);

        if(hit.collider != null)
        {
            grapplePoint = hit.point;
            _linerenderer.SetPosition(0, grapplePoint);
            _linerenderer.SetPosition(1, transform.position);
            _linerenderer.enabled = true;
            playerMovement.enabled = false;
            isGrappling = true;

            _rigidbody.isKinematic = true;
        }
      
    }

    void StopGrapple()
    {
        _linerenderer.enabled = false;
        playerMovement.enabled = true;
        isGrappling = false;

        _rigidbody.isKinematic = false;
    }

    void PullPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, grapplePoint, pullSpeed * Time.deltaTime);

        _linerenderer.SetPosition(1, transform.position);

        if(Vector2.Distance(transform.position, grapplePoint) < 0.1f)
        {
            isGrappling = false;
            StopGrapple();
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling_Gun : MonoBehaviour
{
    [Header("Scripts Reference:")]
    public Grappling_Rope grappleRope;

    [Header("Layer Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappleLayerNumber = 0;

    [Header("Main Camera:")]
    public Camera _camera;

    [Header("Transform Reference:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Reference:")]
    public SpringJoint2D springJoint2D;
    public Rigidbody2D rb;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)][SerializeField] private float rotationSpeed = 4;


    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;


    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 0;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    // Start is called before the first frame update
    void Start()
    {
        grappleRope.enabled = false;
        springJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RopeInputHandler();
    }

    void RopeInputHandler()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            SetGrapplePoint();
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            if(grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousPos, true);
            }
            
            if(launchToPoint && grappleRope.isGrappling)
            {
                if(launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistance = firePoint.position - gunHolder.position;
                    Vector2 targetPos = grapplePoint - firePointDistance;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }

        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            grappleRope.enabled = false;
            springJoint2D.enabled = false;
            rb.gravityScale = 1;
        }
        else
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if(rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void SetGrapplePoint()
    {
        Vector2 distanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if(Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            {
                if(hit.transform.gameObject.layer == grappleLayerNumber || grappleToAll)
                {
                    if(Vector2.Distance(hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                    {
                        grapplePoint = hit.point;
                        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                        grappleRope.enabled = true;
                    }
                }
            }
        }
    }
    public void Grapple()
    {
        springJoint2D.autoConfigureDistance = false;
        if(!launchToPoint && !autoConfigureDistance)
        {
            springJoint2D.distance = targetDistance;
            springJoint2D.frequency = targetFrequency;
        }
        if(!launchToPoint)
        {
            if(autoConfigureDistance)
            {
                springJoint2D.autoConfigureDistance = true;
                springJoint2D.frequency = 0;
            }

            springJoint2D.connectedAnchor = grapplePoint;
            springJoint2D.enabled = true;
        }
        else
        {
            switch(launchType)
            {
                case LaunchType.Physics_Launch:
                    springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    springJoint2D.distance = distanceVector.magnitude;
                    springJoint2D.frequency = launchSpeed;
                    springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    rb.gravityScale = 0;
                    rb.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}

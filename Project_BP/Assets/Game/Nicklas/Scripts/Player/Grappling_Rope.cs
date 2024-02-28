using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling_Rope : MonoBehaviour
{
    [Header("General References:")]
    public Grappling_Gun grapplingGun;
    public LineRenderer lineRender;

    [Header("General Settings:")]
    [SerializeField] private int precision = 40;
    [Range(0, 20)][SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)][SerializeField] private float startWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField][Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool straightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        lineRender.positionCount = precision;
        waveSize = startWaveSize;
        straightLine = false;

        LinePointsToFirePoint();

        lineRender.enabled = true;
    }

    private void OnDisable()
    {
        lineRender.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            lineRender.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if(!straightLine)
        {
            if(lineRender.GetPosition(precision - 1).x == grapplingGun.grapplePoint.x)
            {
                straightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if(!isGrappling)
            {
                grapplingGun.Grapple();
                isGrappling = true;
            }
            if(waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;
                if(lineRender.positionCount != 2) { lineRender.positionCount = 2; }
                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for(int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            lineRender.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        lineRender.SetPosition(0, grapplingGun.firePoint.position);
        lineRender.SetPosition(1, grapplingGun.grapplePoint);
    }
}

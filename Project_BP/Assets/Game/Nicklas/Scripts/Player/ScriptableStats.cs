using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableStats : ScriptableObject
{
    [Header("LAYERS")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;

    [Header("INPUT")]
    [Tooltip("Makes all inpout snap to an integer. Prevents gamepads from walking slowly. Recomended value is true to ensure gamepad/keyboard parity")]
    public bool SnapInput = true;
    
    [Tooltip("Minimum inout required before you mount a ladder or climb a ledge. Avoids unwanted climbing using controllers"), Range(0.01f, 0.99f)]
    public float VerticalDeadZoneThresold = 0.3f;

    [Tooltip("Minimum inout required before a left or right is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
    public float HorizontalDeadZoneThreshold = 0.1f;

    [Header("MOVEMENT")]
    [Tooltip("The top of horizontal movement speed")]
    public float MaxSpeed = 14f;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float Acceleration = 120f;

    [Tooltip("The pace at which the player come to a stop")]
    public float GroundDeceleration = 60f;

    [Tooltip("Deceleration in air only after stopping input mid-air")]
    public float AirDeceleration = 30f;

    [Tooltip("A constant downard force applied while grounded. Helps on slopes"), Range(0f, -10f)]
    public float GroundingForce = -1.5f;

    [Tooltip("The detection distance for grounding and roof detection"), Range(0f, 0.5f)]
    public float GrounderDistance = 0.05f;

    [Header("Jump")]
    [Tooltip("The immediate velocity applied when jumping")]
    public float JumpPower = 36f;

    [Tooltip("The Maxium vertical movement speed")]
    public float MaxFallSpeed = 40f;

    [Tooltip("The player's capacity tog ain fall speed. a.k.a. In Air Gravity")]
    public float FallAcceleration = 110f;

    [Tooltip("The gravity multipler added when jump is released early")]
    public float JumpEndEarlyGravityModifier = 3f;

    [Tooltip("The time before coyote jump becomes unusalbe. Coyote jump allows jump to execute even after leaving a ledge")]
    public float CoyoteTime = 0.15f;

    [Tooltip("The amount of time we buffer a jump. This allows jump input before actually hitting the ground")]
    public float JumpBuffer = 0.2f;
} 

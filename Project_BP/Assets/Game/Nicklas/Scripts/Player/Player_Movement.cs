using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight;
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;


    //Animation
    private Animator anim;
    private string currentAnimation;
    //Animation States
    const string PLAYER_IDLE = "Player_IdleAN";
    const string PLAYER_WALK = "Player_WalkAN";
    const string PLAYER_JUMP = "Player_JumpAN";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Jumping();
        MovementAnimation();
        Flip();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        
        rb.velocity = new Vector2(horizontal * walkSpeed, rb.velocity.y);
    }

    private void Jumping()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if(isFacingRight && horizontal > 0 || !isFacingRight && horizontal < 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
    private void MovementAnimation()
    {
        if(IsGrounded())
        {
            if(rb.velocity.x != 0)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        if(!IsGrounded())
        {
            ChangeAnimationState(PLAYER_JUMP);
        }
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentAnimation == newState) return;
        anim.Play(newState);
        currentAnimation = newState;
    }
}

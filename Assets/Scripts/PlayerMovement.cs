using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    float xMovement;

    [Header("[Jump Settings]")]
    
    [SerializeField] float jumpPower = 10;
    [SerializeField] float fallAcceleration = 0.3f;

    [SerializeField] float coyoteTime = 1;
    float coyoteTimeCounter;

    [SerializeField] float jumpBuffer = 0.2f;
    float jumpBufferCounter;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");

        Jump();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(xMovement * speed, rb2d.velocity.y);
    }


    void Jump()
    {
        if(IsGrounded()) // Coyote timer 
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump")) // Jump buffer timer
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(Input.GetButtonUp("Jump")) coyoteTimeCounter = 0; // Prevents double jumps from coyote time

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0) // Jump 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }

        if(Input.GetButtonUp("Jump") && rb2d.velocity.y > 0) // If player wants short jump
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * fallAcceleration);
        }

        if(!IsGrounded() && rb2d.velocity.y < 0.5f && rb2d.velocity.y > -2) // Apex of jump has lower gravity
        {
            rb2d.gravityScale = 2;
        }
        else
        {
            rb2d.gravityScale = 4;
        }
    }

    public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPlat : MonoBehaviour
{
    private float movementInputDirection;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isWalking;
    private bool isFacingRight = true;
    [SerializeField] private bool isGrounded;
    private bool canJump;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashForce; //24f
    [SerializeField] private float dashingTime; //0.2f
    [SerializeField] private float dashCooldown; //1f



    public float jumpForce;
    public float movementSpeed;
    public float groundCheckRadius;
    public float varJumpHeightMultiplier;

    public Transform groundCheck;

    public LayerMask whatIsGround;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        CheckInput();
        CheckMovimentDirection();
        CheckSurroundings();
        CheckIfCanJump();
        UpdateAnimations();
    }

    private void FixedUpdate() 
    {
        if (!isDashing)
        {
            ApplyMovement();   
        }
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y <= 0){
            canJump = true;
        }else{
            canJump = false;
        }
    }

    private void CheckMovimentDirection()
    {
        if(isFacingRight && movementInputDirection < 0){
            Flip();
        }else if(!isFacingRight && movementInputDirection > 0){
            Flip();
        }

        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", isDashing);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varJumpHeightMultiplier);
        }
        
        if(Input.GetButtonDown("Dash") && canDash && movementInputDirection != 0)
        {
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float gravityDefault = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(movementInputDirection * dashForce, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = gravityDefault;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    private void ApplyMovement()
    {
        rb.velocity = new Vector2( movementSpeed * movementInputDirection, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);    
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    private int facingDirection, damageDirection;

    private Vector2 movement, touchDamageBotLeft, touchDamageTopRight;

    [SerializeField] private float groundCheckDistance, wallCheckDistance, movementSpeed, maxHealth, knockbackDuration, lastTouchDamageTime, touchDamageCooldown, touchDamage, touchDamageWidth, touchDamageHeight;

    [SerializeField] private Transform groundCheck, wallCheck, touchDamageCheck;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private Vector2 knockbackSpeed;

    private bool groundDetected, wallDetected, canMove = true, isKnockback;

    private float currentHealth, knockbackStartTime;

    private float[] attackDetails = new float[2];

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        facingDirection = 1;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
        // Debug.Log(movement);
    }

    //WALKING STATE ----------------------
    private void EnterWalkingState()
    {
        canMove = true;
    }

    private void UpdateWalkingState()
    {
        if(canMove)
        {
            groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

            CheckTouchDamage();

            if(!groundDetected || wallDetected)
            {
                Flip();
            }
            else
            {
                if(!isKnockback)
                {
                    movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
                    aliveRb.velocity = movement;
                }
                
            }
        }
    }

    private void ExitWalkingState()
    {
        canMove = false;
    }

    //KNOCKBACK STATE ----------------------
    private void EnterKnockbackState()
    {
        isKnockback = true;
        canMove = false;
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        if(Time.time > knockbackStartTime + knockbackDuration && groundDetected)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
        isKnockback = false;
    }

    //DEAD STATE ----------------------
    private void EnterDeadState()
    {
        aliveAnim.SetTrigger("isDead");
        Invoke("ExitDeadState", 2f);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {
        Destroy(gameObject);
    }

    //OTHER FUNCTIONS ---------------------

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        
        if(attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hitparticle

        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if(Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if(hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch(state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y)); 

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}

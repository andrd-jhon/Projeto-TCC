using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled, defenseEnabled;
    [SerializeField] private float inputTimer, attack1Radius, attack1Damage, recoveryDefenseCooldown;
    [SerializeField] Transform attack1HitBoxPos, defenseHitPos;
    [SerializeField] LayerMask whatIsDamagable;
    public bool isAttacking, isGroundAttacking, airAttacked, isDefending;
    [SerializeField] private float stepForce;
    [SerializeField] private float timeStep;
    [SerializeField] private int currentAttack, totalDefensesNumber, defensesRemaining;
    private float lastDefenseTime;
    private bool gotInput, gotInputDefense;
    private float[] attackDetails = new float[2];
    // private int currentDefense;
    // private int defensesRemaining; // DEIXAR

    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D _collider;

    private PlayerControllerPlat PC;
    private PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PC = GetComponent<PlayerControllerPlat>();
        PS = GetComponent<PlayerStats>();
        _collider = GetComponent<Collider2D>();
        anim.SetBool("canAttack", combatEnabled);
        defensesRemaining = totalDefensesNumber;
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
        CheckDefense();
        CheckAttacking();
        CheckIsGrounded();
        RecoveryDefense();
    }

    private void FixedUpdate() {
        
    }

    private void CheckIsGrounded()
    {
        if(PC.isGrounded) airAttacked = false;
    }

    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(combatEnabled)
            {
                gotInput = true;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            currentAttack = 0;
            gotInput = false;
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(defenseEnabled)
            {
                gotInputDefense = true;
                
            }
        }

        if(Input.GetMouseButtonUp(1))
        {
            if(defenseEnabled)
            {
                gotInputDefense = false;
                combatEnabled = true;
                isDefending = false;
                anim.SetBool("isDefending", isDefending);
            }
        }
    }

    // private void CheckDefenseInput()
    // {
    //     if(Input.GetMouseButtonDown(1))
    //     {
    //         if(defenseEnabled)
    //         {
    //             gotInputDefense = true;
                
    //         }
    //     }
    // }

    private void CheckAttacks()
    {
        if(gotInput)
        {
            defenseEnabled = false;
            if(!isAttacking && currentAttack == 0 && PC.isGrounded)
            {
                ExecuteAttack1();
            }
            else if(!isAttacking && !PC.isGrounded)
            {
                ExecuteAttackAir();
            }
        }
        
    }

    private void CheckDefense()
    {
        if(gotInputDefense)
        {
            if(PC.isGrounded && !isAttacking && defenseEnabled && defensesRemaining > 0)
            {
                combatEnabled = false;
                if(!isDefending)
                {
                    isDefending = true;
                    anim.SetBool("isDefending", isDefending);
                }
            }
            else
            {
                combatEnabled = true;
                isDefending = false;
                anim.SetBool("isDefending", isDefending);
            }
            
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamagable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void CheckDefenseHitBox()
    {
        // Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamagable);
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(defenseHitPos.position, 0.75f, whatIsDamagable);

        attackDetails[0] = 0;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void RecoveryDefense()
    {
        if(defensesRemaining < totalDefensesNumber)
        {
            if(Time.time >= lastDefenseTime + recoveryDefenseCooldown)
            {
                Debug.Log("O tempo é maior");
                defensesRemaining++;
                if(defensesRemaining < totalDefensesNumber) lastDefenseTime = Time.time;
            }
        }
    }

    private void FinishAttack() //CHAMADO NA ANIMAÇÃO
    {   
        if(gotInput && PC.isGrounded){
            
            switch(currentAttack)
            {
                case 0:
                    ExecuteAttack1();
                    break;
                case 1:
                    ExecuteAttack2();
                    break;
                case 2:
                    ExecuteAttack3();
                    break;
                case 3:
                    isAttacking = false;
                    anim.SetBool("isAttacking", isAttacking);
                    currentAttack = 0;
                break;
            }
        }
        else
        {
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
            currentAttack = 0;
            isGroundAttacking = false;
            defenseEnabled = true;
        }
    }

    private void Damage(float[] attackDetails)
    {
        if(!PC.GetDashStatus())
        {
            if(isDefending && attackDetails[1] < transform.position.x && !PC.isFacingRight || isDefending && attackDetails[1] > transform.position.x && PC.isFacingRight)
            {
                CheckDefenseHitBox();
                if(defensesRemaining > 0) defensesRemaining--;
                lastDefenseTime = Time.time;
            }
            else
            {
                int direction;

                PS.DecreaseHealth(attackDetails[0]);

                if(attackDetails[1] < transform.position.x)
                {
                    direction = 1;
                }
                else
                {
                    direction = -1;
                }

                PC.Knockback(direction);
                isAttacking = false;
            }
        }
    }

    private void CheckAttacking()
    {
        PC.isAttacking = isGroundAttacking;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
        Gizmos.DrawWireSphere(defenseHitPos.position, 0.75f);
    }

    private void ExecuteAttack1()
    {
        isAttacking = true;
        isGroundAttacking = true;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isFirstAttack", true);
        anim.SetBool("isSecondAttack", false);
        anim.SetBool("isThirdAttack", false);
        currentAttack++;
        rb.velocity = new Vector2(0, rb.velocityY);
    }

    private void ExecuteAttack2()
    {
        isAttacking = true;
        isGroundAttacking = true;
        anim.SetBool("isSecondAttack", true);
        anim.SetBool("isFirstAttack", false);
        anim.SetBool("isThirdAttack", false);
        currentAttack++;
        // rb.AddForce(new Vector2(stepForce, rb.velocityY));
        StartCoroutine(StepAttack());
    }

    private void ExecuteAttack3()
    {
        isAttacking = true;
        isGroundAttacking = true;
        anim.SetBool("isThirdAttack", true);
        anim.SetBool("isFirstAttack", false);
        anim.SetBool("isSecondAttack", false);
        currentAttack++;
        // rb.AddForce(new Vector2(stepForce, rb.velocityY));
        StartCoroutine(StepAttack());
    }

    private void ExecuteAttackAir()
    {
        if(!airAttacked)
        {
            isAttacking = true;
            airAttacked = true;
            anim.SetBool("isAttacking", isAttacking);
        }
    }

    private IEnumerator StepAttack()
    {
        float elapsedTime = 0f;
        float duration = timeStep; // Duração do movimento
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + transform.right * stepForce; // Define o alvo do movimento

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration); // Movimenta suavemente
            elapsedTime += Time.deltaTime;
            yield return null; // Espera o próximo frame
        }
        transform.position = targetPosition; // Garante que a posição final seja exatamente a posição de destino
        isAttacking = false;
    }
}

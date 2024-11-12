using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField] Transform attack1HitBoxPos;
    [SerializeField] LayerMask whatIsDamagable;
    public static bool isAttacking;
    [SerializeField] private float stepForce;
    [SerializeField] private float timeStep;
    [SerializeField] private int currentAttack;
    private bool gotInput;
    private float[] attackDetails = new float[2];

    private Animator anim;
    private Rigidbody2D rb;

    private PlayerControllerPlat PC;
    private PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PC = GetComponent<PlayerControllerPlat>();
        anim.SetBool("canAttack", combatEnabled);
        PS = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
        CheckAttacking();
    }

    private void FixedUpdate() {
        
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
    }

    private void CheckAttacks()
    {
        if(gotInput)
        {
            if(!isAttacking && currentAttack == 0 && PlayerControllerPlat.isGrounded)
            {
                ExecuteAttack1();
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

    private void FinishAttack()
    {   
        if(gotInput){
            
            switch(currentAttack)
            {
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
            
        }
    }

    private void Damage(float[] attackDetails)
    {
        if(!PC.GetDashStatus())
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
        }

        
    }

    private void CheckAttacking()
    {
        PlayerControllerPlat.canMove = !isAttacking;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

    private void ExecuteAttack1()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isFirstAttack", true);
        anim.SetBool("isSecondAttack", false);
        anim.SetBool("isThirdAttack", false);
        currentAttack++;
        rb.velocity = new Vector2(0, rb.velocityY);
    }

    private void ExecuteAttack2()
    {
        anim.SetBool("isSecondAttack", true);
        anim.SetBool("isFirstAttack", false);
        anim.SetBool("isThirdAttack", false);
        currentAttack++;
        // rb.AddForce(new Vector2(stepForce, rb.velocityY));
        StartCoroutine(StepAttack());
    }

    private void ExecuteAttack3()
    {
        anim.SetBool("isThirdAttack", true);
        anim.SetBool("isFirstAttack", false);
        anim.SetBool("isSecondAttack", false);
        currentAttack++;
        // rb.AddForce(new Vector2(stepForce, rb.velocityY));
        StartCoroutine(StepAttack());
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
}
}

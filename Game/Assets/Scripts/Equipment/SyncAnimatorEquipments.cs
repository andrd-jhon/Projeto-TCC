using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncAnimatorEquipments : MonoBehaviour
{
    private Animator playerAnimator; // O Animator do Player
    public List<Animator> equipmentAnimators; // Lista de Animators dos equipamentos
    private string lastStateName;


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();

        Animator[] childAnimators = GetComponentsInChildren<Animator>();

        foreach (Animator animator in childAnimators)
        {
            if (animator != playerAnimator)
            {
                equipmentAnimators.Add(animator);
            }
        }
    }

    private void Update()
    {
        SyncEquipmentAnimators();
    }

    private void SyncEquipmentAnimators()
    {
        AnimatorStateInfo playerStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        string currentStateName = playerStateInfo.IsName("Idle") ? "Idle" :
                                  playerStateInfo.IsName("Walk") ? "Walk" :
                                  playerStateInfo.IsName("Attack1") ? "Attack1" :
                                  playerStateInfo.IsName("Attack2") ? "Attack2" :
                                  playerStateInfo.IsName("Attack3") ? "Attack3" :
                                  playerStateInfo.IsName("AttackAir") ? "AttackAir" :
                                  playerStateInfo.IsName("Jump") ? "Jump" :
                                  playerStateInfo.IsName("Fall") ? "Fall" :
                                  playerStateInfo.IsName("Hurt") ? "Hurt" :
                                  playerStateInfo.IsName("Defense") ? "Defense" :
                                  playerStateInfo.IsName("DefenseWalk") ? "DefenseWalk" :
                                   "None";

        // Se o estado do Player mudou, atualiza os Animators dos equipamentos
        if (currentStateName != lastStateName)
        {
            lastStateName = currentStateName;

            foreach (Animator equipmentAnimator in equipmentAnimators)
            {
                
                // Atualiza os parâmetros dos equipamentos de acordo com o novo estado
                equipmentAnimator.SetBool("isIdle", currentStateName == "Idle");
                equipmentAnimator.SetBool("isWalking", currentStateName == "Walk");
                equipmentAnimator.SetBool("isAttacking1", currentStateName == "Attack1");
                equipmentAnimator.SetBool("isAttacking2", currentStateName == "Attack2");
                equipmentAnimator.SetBool("isAttacking3", currentStateName == "Attack3");
                equipmentAnimator.SetBool("isAttackingAir", currentStateName == "AttackAir");
                equipmentAnimator.SetBool("isJumping", currentStateName == "Jump");
                equipmentAnimator.SetBool("isFalling", currentStateName == "Fall");
                equipmentAnimator.SetBool("isHurt", currentStateName == "Hurt");
                equipmentAnimator.SetBool("isDefending", currentStateName == "Defense");
                equipmentAnimator.SetBool("isDefendingWalking", currentStateName == "DefenseWalk");
            }
        }
    }
}

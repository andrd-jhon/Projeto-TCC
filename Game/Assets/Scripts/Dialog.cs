using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public DialogueData dialogueData;

    // int currentText = 0;
    // bool finished = false;
    bool playerIsClose;
    bool isTalk = false;
    public bool conversational;

    // TypeTextAnimation typeText;
    DialogSystem dialogSystem;
    DialogUI dialogUI;
    PlayerController playerController;
    // GameObject dialog;

    private void Awake() 
    {
        dialogSystem = FindObjectOfType<DialogSystem>();
        dialogUI = FindObjectOfType<DialogUI>();
        playerController = FindObjectOfType<PlayerController>();
        // dialog = playerController.collidedOBJ;
    }

    private void Start() 
    {

    }
    
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            conversational = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            conversational = false;
        }
        
    }

    public void Conversation()
    {
        if(playerController.playerIsClose){
            if (!isTalk)
            {
                dialogSystem.Next();
                isTalk = true;
            }
            else
            {
                isTalk = false;
            }
        }
    }

    private void Update() 
    {
        
    }

// ISTO É A ULTIMA VERSÃO 
//     public void Interact()
// {
//     if (Input.GetKeyDown(KeyCode.E))
//     {
//         if (!isTalk && playerIsClose)
//         {
//             dialogSystem.Next();
//             isTalk = true;
//         }
//         else
//         {
//             isTalk = false;
//         }
//     }
// }

    // public void Conversation()
    // {
    //     dialog = playerController.collidedOBJ;
    //     if(!isTalk)
    //     {
    //         dialogSystem.Next();
    //         isTalk = true;
    //     }
    //     else
    //     {
    //         isTalk = false;
    //     }
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dialog : MonoBehaviour,IInteractable
{
    public UnityEvent finishDialog;
    public DialogueData dialogueData;
    DialogSystem dialogSystem;

    bool isTalk = false;

    private void Awake() 
    {
        dialogSystem = FindObjectOfType<DialogSystem>();
    }

    public void Interact()
    {
        Conversation();
    }

    public void Conversation()
    {
        if(PlayerController.playerIsClose && dialogSystem.npcDialog != null){
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
        else return;
    }
}

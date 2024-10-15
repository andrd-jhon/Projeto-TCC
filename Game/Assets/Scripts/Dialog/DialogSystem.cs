using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE { DISABLED, WAITING, TYPING }


public class DialogSystem : MonoBehaviour
{
    int currentText = 0;
    public bool finished = false;

    GameObject NPC;
    public Dialog npcDialog;
    DialogueData dialogueData;
    [SerializeField] private TypeTextAnimation typeText;
    DialogUI dialogUI;
    PlayerController playerController;



    public STATE state;
    
    private void Awake() 
    {
        // typeText = FindObjectOfType<TypeTextAnimation>(); 
        dialogUI = FindObjectOfType<DialogUI>();
        dialogueData = FindObjectOfType<DialogueData>();
        playerController = FindObjectOfType<PlayerController>();

        typeText.TypeFinished = OnTypeFinishe;

    }

    void Start()
    {
        state = STATE.DISABLED;
    }

    void Update()
    {
        NPC = playerController.collidedOBJ;

        if(NPC != null)
        {
            npcDialog = NPC.GetComponent<Dialog>();
        }

        if(state == STATE.DISABLED) return;

        switch (state)
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
        }
    }

    void OnTypeFinishe(){
        state = STATE.WAITING;
    }
    
    public void Next()
    {
        if (npcDialog == null) return;

        if(currentText == 0){
            dialogUI.Enable();
        }
        
        dialogUI.SetCharacter(npcDialog.dialogueData.talkScript[currentText].name1,
                              npcDialog.dialogueData.talkScript[currentText].character1,
                              npcDialog.dialogueData.talkScript[currentText].name2,
                              npcDialog.dialogueData.talkScript[currentText].character2);

        IsTalking();

        typeText.speech = npcDialog.dialogueData.talkScript[currentText++].text;
        if(currentText == npcDialog.dialogueData.talkScript.Count) finished = true;
        typeText.StartTyping();
        state = STATE.TYPING;
    }

    void Waiting()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            if(!finished){
                Next();
            }
            else
            {
                dialogUI.Disable();
                state = STATE.DISABLED;
                currentText = 0;
                finished = false;
                PlayerController.state = PLAYER.FREE;
                Debug.Log(PLAYER.FREE);
                npcDialog.finishDialog?.Invoke();
            }
        }
    }

    void Typing()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            typeText.Skip();
            state = STATE.WAITING;
        }
    }

    public void IsTalking(){
        if(npcDialog.dialogueData.talkScript[currentText].LeftTalk){
            dialogUI.character1.enabled = true;
            dialogUI.character2.enabled = false;
        }else{
            dialogUI.character2.enabled = true;
            dialogUI.character1.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject interface1;
    [SerializeField] private GameObject interface2;

    [SerializeField] private PopUpOrder popUpOrder;

    public List<StepSlot> allStepSlot = new List<StepSlot>();

    [SerializeField] private GameObject PopUpUI;

    [SerializeField] MouseFollowerPopUp mouseFollowerPopUp;
    private StepSlot originSlotStep;

    [SerializeField] private TMP_Text mechanicName;
    [SerializeField] private Image imageMechanic;
    [SerializeField] private TMP_Text description;

    private bool isActive;

    private void Start()
    {
        SetData();
        MouseEvents();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q) && isActive){
            PopUpUI.SetActive(false);
            PlayerController.state = PLAYER.FREE;
            isActive = false;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && !isActive){
            Debug.Log(PopUpUI.name);
            PlayerController.state = PLAYER.INTERACT;
            PopUpUI.SetActive(true);
            isActive = true;
        }
        
        if(Input.GetKeyDown(KeyCode.D) && isActive){
            ChangeInterfaceTo2();
        }
        if(Input.GetKeyDown(KeyCode.A) && isActive){
            ChangeInterfaceTo1();
        }

    }

    private void MouseEvents()
    {
        foreach (StepSlot slotStep in allStepSlot)
        {
            slotStep.OnStepBeginDrag += HandleBeginDrag;
            slotStep.OnStepDrop += HandleSwap;
            slotStep.OnStepEndDrag += HandleEndDrag;
        }
    }

    private void SetData()
    {
        mechanicName.text = popUpOrder.popUpData.mechanicName;
        imageMechanic.sprite = popUpOrder.popUpData.mechanicView;
        description.text = popUpOrder.popUpData.mechanicDescription;
    }

    private void HandleBeginDrag(StepSlot obj)
    {
        if(obj.isFilled){
        mouseFollowerPopUp.Toggle(true);
        mouseFollowerPopUp.SetData(obj.stepText.text);
        originSlotStep = obj;
        }else return;
    }

    private void HandleSwap(StepSlot targetSlotStep)
    {
        if(targetSlotStep == null || mouseFollowerPopUp.currentStepText == "") return;
        if(targetSlotStep.isFilled)
        {
            string tempTargetSlotStep = targetSlotStep.stepText.text;
            targetSlotStep.InsertStep(originSlotStep.stepText.text);
            originSlotStep.InsertStep(tempTargetSlotStep);
        }else
        {
            targetSlotStep.InsertStep(originSlotStep.stepText.text);
            originSlotStep.RemoveStep();
        }
        popUpOrder.stepsFillable.Clear();
        popUpOrder.GetSlotStep();
    }

    private void HandleEndDrag(StepSlot obj)
    {
        mouseFollowerPopUp.Toggle(false);
    }

    private void ChangeInterfaceTo2(){
        interface1.SetActive(false);
        interface2.SetActive(true);
    }
    private void ChangeInterfaceTo1(){
        interface2.SetActive(false);
        interface1.SetActive(true);
    }
    
}
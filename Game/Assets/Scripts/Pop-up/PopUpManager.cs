using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpManager : MonoBehaviour
{
    public GameObject interface1;
    public GameObject interface2;

    public GameObject slot;
    public Transform parentTransformToFill;
    public Transform parentTransformFillable;

    public List<StepSlot> allStepSlot = new List<StepSlot>();

    [SerializeField] private GameObject PopUpUI;

    [SerializeField] MouseFollowerPopUp mouseFollowerPopUp;
    private StepSlot originSlotStep;

    public PopUpData popUpData;

    [SerializeField] private TMP_Text mechanicName;
    [SerializeField] private Image imageMechanic;
    [SerializeField] private TMP_Text description;

    private bool isActive;

    private void Awake() {
        // playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        AddSlotsToFill();
        AddSlotsFillable();
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
        mechanicName.text = popUpData.mechanicName;
        imageMechanic.sprite = popUpData.mechanicView;
        description.text = popUpData.mechanicDescription;
    }

    private void AddSlotsToFill()
    {
        int number = 0;

        foreach (Step step in popUpData.stepsQuantity)
        {
            GameObject instantiatedObject = Instantiate(slot, parentTransformToFill);
            StepSlot stepSlot = instantiatedObject.GetComponent<StepSlot>();
            allStepSlot.Add(stepSlot);
            // stepSlot.imageFill.enabled = true;
            stepSlot.show.SetActive(true);
            stepSlot.stepText.text = step.stepText;
            stepSlot.name = "StepToFill" + number.ToString();
            number++;
        }
    }
    private void AddSlotsFillable()
    {
        int number = 0;

        foreach (Step step in popUpData.stepsQuantity)
        {
            GameObject instantiatedObject = Instantiate(slot, parentTransformFillable);
            StepSlot stepSlot = instantiatedObject.GetComponent<StepSlot>();
            allStepSlot.Add(stepSlot);
            stepSlot.name = "StepFillable" + number.ToString();
            number++;

        }
    }

    private void HandleBeginDrag(StepSlot obj)
    {
        Debug.Log("SEGURANDO");
        mouseFollowerPopUp.Toggle(true);
        mouseFollowerPopUp.SetData(obj.stepText.text);
        originSlotStep = obj;
    }

    private void HandleSwap(StepSlot targetSlotStep)
    {
        Debug.Log("DROPANDO");
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
    }

    private void HandleEndDrag(StepSlot obj)
    {
        Debug.Log("SOLTANDO");
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

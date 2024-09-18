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

    [SerializeField]
    private GameObject PopUpUI;

    public PopUpData popUpData;

    [SerializeField] private TMP_Text mechanicName;
    [SerializeField] private Image imageMechanic;
    [SerializeField] private TMP_Text description;

    private PlayerController playerController;

    private bool isActive;

    private void Awake() {
        playerController = FindObjectOfType<PlayerController>();
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
        foreach (Step step in popUpData.stepsQuantity)
        {
            GameObject instantiatedObject = Instantiate(slot, parentTransformToFill);
            StepSlot stepSlot = instantiatedObject.GetComponent<StepSlot>();
            allStepSlot.Add(stepSlot);
            // stepSlot.imageFill.enabled = true;
            stepSlot.show.SetActive(true);
            stepSlot.stepText.text = step.stepText;
        }
    }
    private void AddSlotsFillable()
    {
        foreach (Step step in popUpData.stepsQuantity)
        {
            GameObject instantiatedObject = Instantiate(slot, parentTransformFillable);
            // StepSlot stepSlot = instantiatedObject.GetComponent<StepSlot>();
        }
    }

    private void HandleBeginDrag(StepSlot obj)
    {
        Debug.Log("SEGURANDO");
    }

    private void HandleSwap(StepSlot obj)
    {
        
    }

    private void HandleEndDrag(StepSlot obj)
    {
        Debug.Log("SOLTANDO");
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

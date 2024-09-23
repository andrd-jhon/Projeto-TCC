using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopUpOrder : MonoBehaviour
{
    public List<string> stepsFillable = new List<string>();
    public PopUpData popUpData;
    public GameObject slot;
    public Transform parentTransformToFill;
    public Transform parentTransformFillable;
    [SerializeField] private PopUpManager popUpManager;

    public List<string> correctOrderSteps = new List<string>();

    private bool isEqual;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.V)){
            Verify();
        }
    }

    private void Awake() {
        AddSlotsToFill();
        AddSlotsFillable();
    }

    private void Start() {
        SetCorrectOrder();
    }

    private void AddSlotsToFill()
    {
        int number = 0;

        foreach (Step step in popUpData.stepsQuantity)
        {
            GameObject instantiatedObject = Instantiate(slot, parentTransformToFill);
            StepSlot stepSlot = instantiatedObject.GetComponent<StepSlot>();
            popUpManager.allStepSlot.Add(stepSlot);
            stepSlot.isFilled = true;
            stepSlot.show.SetActive(true);
            stepSlot.stepText.text = step.stepText;
            stepSlot.name = "StepToFill" + number.ToString();
            number++;
        }
        number = 0;
    }

    private void AddSlotsFillable()
    {
        int number = 0;

        foreach (Step step in popUpData.stepsQuantity)
        {
            GameObject instantiatedObject = Instantiate(slot, parentTransformFillable);
            StepSlot stepSlot = instantiatedObject.GetComponent<StepSlot>();
            popUpManager.allStepSlot.Add(stepSlot);
            stepSlot.name = "StepFillable" + number.ToString();
            number++;
        }
        number = 0;
    }

    private void SetCorrectOrder(){
        foreach(Step step in popUpData.stepsQuantity)
        {
            correctOrderSteps.Add(step.stepText);
        }
    }

    public void GetFillables(){
        StepSlot[] stepSlots = parentTransformFillable.GetComponentsInChildren<StepSlot>();
        foreach (StepSlot slot in stepSlots)
        {
            stepsFillable.Add(slot.stepText.text);
        }
    }

    public void Verify()
    {
        isEqual = stepsFillable.SequenceEqual(correctOrderSteps);
        if(isEqual){
            Debug.Log("ESTÁ IGUALZINHO");
        }else{
            Debug.Log("ta diferente bro");
        }
    } 

}
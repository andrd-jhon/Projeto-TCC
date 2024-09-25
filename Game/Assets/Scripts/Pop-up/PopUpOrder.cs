using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PopUpOrder : MonoBehaviour
{
    public List<string> stepsFillable = new List<string>();
    public PopUpData popUpData;
    public GameObject slot;
    public Transform parentTransformToFill;
    public Transform parentTransformFillable;
    [SerializeField] private PopUpManager popUpManager;
    [SerializeField] private GameObject checkButton;

    private StepSlot[] stepSlotsFillable;
    private StepSlot[] stepSlotsToFill;

    [SerializeField] private Image attempt1;
    [SerializeField] private Image attempt2;
    [SerializeField] private Image attempt3;
    private int attemptNumber;
    public int reward;

    private List<string> correctOrderSteps = new List<string>();

    private bool isEqual;
    private bool verifiable = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.V)){
            Verify();
        }
    }

    private void Awake() {
        AddSlotsToFill(); //da pra unir esse
        AddSlotsFillable(); //e esse
        SetCorrectOrder();
    }

    private void Start() {
        
        stepSlotsFillable = parentTransformFillable.GetComponentsInChildren<StepSlot>();
        stepSlotsToFill = parentTransformToFill.GetComponentsInChildren<StepSlot>();
        reward = popUpData.totalReward;
    }

    private void AddSlotsToFill()
    {
        int number = 0;

        List<Step> shuffledSteps = new List<Step>(popUpData.stepsQuantity);
        Shuffle(shuffledSteps);

        foreach (Step step in shuffledSteps)
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
    }

    private void Shuffle(List<Step> list)
    {
        for(int i=0; i<list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            Step temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
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

    public void UpdateSlotStep(){
        foreach (StepSlot slot in stepSlotsFillable)
        {
            stepsFillable.Add(slot.stepText.text);
        }
    }

    public void ButtonUpdate(){
        foreach (StepSlot slot in stepSlotsFillable)
        {
            if(!slot.isFilled)
            {
                checkButton.SetActive(false);
                break;
            }
            checkButton.SetActive(true);
        }
    }

    public void Verify()
    {
        isEqual = stepsFillable.SequenceEqual(correctOrderSteps);
        if (isEqual){
            Debug.Log("ORDEM CORRETA! voce ganhou " + reward + " de recompensa");
        }
        else
        {
            Debug.Log("ORDEM ICORRETA");
            attemptNumber++;
            for(int i = 0; i < stepsFillable.Count; i++)
            {
                if(i >= correctOrderSteps.Count || (stepsFillable[i] != correctOrderSteps[i] && !string.IsNullOrEmpty(stepsFillable[i])))
                {
                    Debug.Log(stepSlotsFillable[i].stepText.text);
                    StepSlot availableSlot = stepSlotsToFill.FirstOrDefault(slot => !slot.isFilled);
                    if (availableSlot != null)
                    {
                        availableSlot.InsertStep(stepsFillable[i]);
                        stepSlotsFillable[i].RemoveStep();
                    }
                }
            }
        }
        checkButton.SetActive(false);
        SetReward();
    }

    private void SetReward()
    {
        switch (attemptNumber)
            {
                case 0:
                    reward = popUpData.totalReward;
                    break;
                case 1:
                    reward = popUpData.secondReward;
                    break;
                case 2:
                    reward = popUpData.thirdReward;
                    break;
                case 3:
                    reward = popUpData.fourthReward;
                    Debug.Log("Não foi dessa vez. Ganhou " + reward + " de recompensa");
                    break;
            }
    }
}
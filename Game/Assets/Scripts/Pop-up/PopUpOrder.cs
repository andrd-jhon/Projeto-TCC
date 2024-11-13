using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.Events;
using TMPro;

public class PopUpOrder : MonoBehaviour
{
    private InformationBox informationBox;

    public List<string> stepsFillable = new List<string>();
    public PopUpData popUpData;
    public GameObject slot;
    public Transform parentTransformToFill;
    public Transform parentTransformFillable;
    [SerializeField] private PopUpManager popUpManager;
    [SerializeField] private GameObject popUpUI;
    [SerializeField] private GameObject checkButton;
    [SerializeField] private GameObject explanation;
    [SerializeField] private TypeTextAnimation textExplanation;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private TMP_Text exitButtonText;

    private StepSlot[] stepSlotsFillable;
    private StepSlot[] stepSlotsToFill;

    [SerializeField] private Image attempt1;
    [SerializeField] private Image attempt2;
    [SerializeField] private Image attempt3;
    private int attemptNumber;
    private bool isShowing;
    public int reward;

    private List<string> correctOrderSteps = new List<string>();

    private bool isEqual;

    public Color redAttempt = new Color(200f / 255, 75f / 255, 75f / 255, 255f / 255);
    private Color greenAttempt = new Color(75f / 255, 220f / 255, 75f / 255, 255f / 255);

    private void Update() {
        if(Input.GetKeyDown(KeyCode.V)){
            Verify();
        }
    }

    private void Awake() {
        AddSlotsToFill();
        AddSlotsFillable();
        SetCorrectOrder();
        informationBox = GetComponent<InformationBox>();
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
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
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

    public void ExplanationOrExit(){
        if(!isShowing){
            foreach(Transform slotsToFill in parentTransformToFill){
                if(slotsToFill.gameObject.activeInHierarchy){
                    GameObject.Destroy(slotsToFill.gameObject);
                }
            }
            ShowCorrect();
            exitButton.SetActive(true);
            exitButtonText.text = "SAIR";
            popUpManager.DesactiveMouseEvents();
            explanation.SetActive(true);
            textExplanation.speech = popUpData.mechanicExplanation;
            textExplanation.StartTyping();
            isShowing = true;
        }else{
            popUpUI.SetActive(false);
            informationBox.informationText = "Você recebeu " + reward +  " de recompensa";
            informationBox.ShowInformationsBox();
            PlayerController.state = PLAYER.FREE;
        }
        
    }
    
    public void Verify()
    {
        Color tempColor;
        isEqual = stepsFillable.SequenceEqual(correctOrderSteps);
        if (isEqual)
        {
            StartCoroutine(popUpManager.VerifyColor(greenAttempt));
            tempColor = greenAttempt;
            ExplanationOrExit();
        }
        else
        {
            StartCoroutine(popUpManager.VerifyColor(redAttempt));
            for (int i = 0; i < stepsFillable.Count; i++)
            {
                if (i >= correctOrderSteps.Count || (stepsFillable[i] != correctOrderSteps[i] && !string.IsNullOrEmpty(stepsFillable[i])))
                {
                    StepSlot availableSlot = stepSlotsToFill.FirstOrDefault(slot => !slot.isFilled);
                
                    if (availableSlot != null)
                    {
                        availableSlot.InsertStep(stepsFillable[i]);
                        stepSlotsFillable[i].RemoveStep();
                    }
                }
            }
            tempColor = redAttempt;
            attemptNumber++;
        }
        checkButton.SetActive(false);
        SetReward(tempColor);
    }

    private void SetReward(Color currentColor)
    {
        switch (attemptNumber)
        {  
            case 0:
                reward = popUpData.totalReward;
                attempt1.color = greenAttempt;
                break;
            case 1:
                reward = popUpData.secondReward;
                attempt1.color = redAttempt;
                if(currentColor == greenAttempt) attempt2.color = greenAttempt;
                break;
            case 2:
                reward = popUpData.thirdReward;
                attempt2.color = redAttempt;
                if(currentColor == greenAttempt) attempt3.color = greenAttempt;
                break;
            case 3:
                reward = popUpData.fourthReward;
                attempt3.color = currentColor;
                exitButton.SetActive(true);
                popUpManager.DesactiveMouseEvents();
                break;
        }
    }

    private void ShowCorrect(){
        for (int i = 0; i < stepsFillable.Count; i++)
            {
                stepSlotsFillable[i].InsertStep(correctOrderSteps[i]);
            }
    }
}
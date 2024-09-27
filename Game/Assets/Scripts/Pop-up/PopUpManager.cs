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

    [SerializeField] private GameObject popUpUI;

    [SerializeField] MouseFollowerPopUp mouseFollowerPopUp;
    private StepSlot originSlotStep;

    [SerializeField] private TMP_Text mechanicName;
    [SerializeField] private Image imageMechanic;
    [SerializeField] private TMP_Text description;
    [SerializeField] private FadeComponent attemptIndicator;

    private bool isActive;

    private void Start()
    {
        SetData();
        MouseEvents();
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

    public void DesactiveMouseEvents()
    {
        foreach (StepSlot slotStep in allStepSlot)
        {
            slotStep.OnStepBeginDrag -= HandleBeginDrag;
            slotStep.OnStepDrop -= HandleSwap;
            slotStep.OnStepEndDrag -= HandleEndDrag;
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
    }

    private void HandleEndDrag(StepSlot obj)
    {
        mouseFollowerPopUp.Toggle(false);
        popUpOrder.UpdateSlotStep();
        popUpOrder.ButtonUpdate();
    }

    public void ChangeInterfaceTo2(){
        interface1.SetActive(false);
        interface2.SetActive(true);
    }
    public void ChangeInterfaceTo1(){
        interface2.SetActive(false);
        interface1.SetActive(true);
    }

    public IEnumerator verifyColor(Color colorAttempt){
        attemptIndicator.fadeImage.enabled = true;
        yield return StartCoroutine(attemptIndicator.Fade(colorAttempt, attemptIndicator.transparentColor, 1f));
        attemptIndicator.fadeImage.enabled = false;
    }
}
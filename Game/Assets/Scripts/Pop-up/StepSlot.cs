using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class StepSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public GameObject show;
    public Image imageFill;
    public TMP_Text stepText;

    private string text; 

    public event Action<StepSlot> OnStepDrop, OnStepBeginDrag, OnStepEndDrag;


    public void InsertData(Step stepData){
        stepText.text = stepData.stepText;
        show.SetActive(true);
    }

    public void RemoveStep(){
        show.SetActive(false);
        stepText.text = "";
    }

    public void OnDrag(PointerEventData eventData){

    }

    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("COMEÇANDO A SEGURAR");
        OnStepBeginDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData){
        OnStepDrop?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData){
        OnStepEndDrag?.Invoke(this);
    }
}

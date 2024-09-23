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
    public bool isFilled;

    // private string text; 

    public event Action<StepSlot> OnStepDrop, OnStepBeginDrag, OnStepEndDrag;


    public void InsertStep(string text){
        isFilled = true;
        stepText.text = text;
        show.SetActive(true);
    }

    public void RemoveStep(){
        isFilled = false;
        show.SetActive(false);
        stepText.text = "";
    }

    public void OnDrag(PointerEventData eventData){

    }

    public void OnBeginDrag(PointerEventData eventData){
        OnStepBeginDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData){
        // Debug.Log("ALFKJNAJN");
        OnStepDrop?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData){
        OnStepEndDrag?.Invoke(this);
    }
}

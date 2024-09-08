using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class EquipmentField : MonoBehaviour, IPointerClickHandler
{
    public string equipmentName;
    public Sprite equipmentSprite;
    public bool isFilled;
    public string description;
    public EquipmentCategory equipmentCategory;

    private bool empty = true;

    [SerializeField]
    private Image equipmentImage;

    public GameObject select;
    public bool equipmentSelected;

    private EquipmentManager equipmentManager;
    public EquipmentInformationsUI equipmentInformationsUI;

    public EquipmentData equipmentData;

    public event Action<EquipmentField> OnEquipmentDroppedOn, OnEquipmentBeginDrag, OnEquipmentEndDrag; //onpointclick;

    private void Awake()
    {
        equipmentManager = GameObject.Find("EquipmentSystem").GetComponent<EquipmentManager>();
    }

    public void InsertEquipment(EquipmentData equipmentData)
    {
        this.equipmentData = equipmentData;
        equipmentName = equipmentData.equipmentName;
        equipmentSprite = equipmentData.image;
        description = equipmentData.description;
        equipmentCategory = equipmentData.equipmentCategory;
        isFilled = true;
        equipmentImage.sprite = equipmentData.image;
        empty = false;
    }

    public void RemoveEquipment()
    {
        this.equipmentData = null;
        equipmentName = "";
        equipmentSprite = null;
        description = "";
        isFilled = false;
        equipmentImage.sprite = null;
        empty = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            equipmentManager.Deselect();
            select.SetActive(true);
                if(equipmentData != null){
                    equipmentInformationsUI.SetInformations(equipmentData);
                }
            equipmentSelected = true;
        }
    }

    public void OnBeginDrag(){
        if(empty){
            // Debug.Log("Esta vazio");
            return;
        }
        else{
            OnEquipmentBeginDrag?.Invoke(this);
        }
    }

    public void OnDrop(){
        OnEquipmentDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(){
        OnEquipmentEndDrag?.Invoke(this);
    }

    // public void onpointerclick(){
    //     onpointclick?.Invoke(this);
    // }


    // private void ResetStates()
    // {
    //     descriptionName.text = "";
    //     descriptionText.text = "";
    //     descriptionImage.sprite = null;
    // }
}

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

    public event Action<EquipmentField> OnEquipmentDrop, OnEquipmentBeginDrag, OnEquipmentEndDrag, onpointclick;

    private void Awake()
    {
        equipmentManager = GameObject.Find("EquipmentSystem").GetComponent<EquipmentManager>();
    }

    public void InsertEquipment(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        this.equipmentName = equipmentName;
        this.equipmentSprite = equipmentSprite;
        this.description = description;
        this.equipmentCategory = equipmentCategory;
        isFilled = true;
        equipmentImage.sprite = equipmentSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            equipmentManager.Deselect();
            select.SetActive(true);
            equipmentInformationsUI.SetInformations(equipmentName, equipmentSprite, description);
            equipmentSelected = true;
        }
    }

    public void OnBeginDrag(){
        if(empty)
            return;
        OnEquipmentBeginDrag?.Invoke(this);
    }

    public void OnDrop(){
        OnEquipmentDrop?.Invoke(this);
    }

    public void OnEndDrag(){
        OnEquipmentEndDrag?.Invoke(this);
    }

    public void onpointerclick(){
        onpointclick?.Invoke(this);
    }


    // private void ResetStates()
    // {
    //     descriptionName.text = "";
    //     descriptionText.text = "";
    //     descriptionImage.sprite = null;
    // }
}

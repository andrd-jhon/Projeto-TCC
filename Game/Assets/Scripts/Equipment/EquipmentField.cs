using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentField : MonoBehaviour, IPointerClickHandler
{
    public string equipmentName;
    public Sprite equipmentSprite;
    public bool isFilled;
    public string description;
    public EquipmentCategory equipmentCategory;

    // [SerializeField]
    // private Sprite defaultImage;

    [SerializeField]
    private Image equipmentImage;

    public Image descriptionImage;
    public TMP_Text descriptionName;
    public TMP_Text descriptionText;

    public GameObject select;
    public bool equipmentSelected;

    private EquipmentManager equipmentManager;

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
            OnLeftClick();
        }
    }

    private void OnLeftClick()
    {
        equipmentManager.Deselect();
        descriptionImage.enabled = true;
        select.SetActive(true);
        equipmentSelected = true;
        descriptionName.text = equipmentName;
        descriptionText.text = description;
        descriptionImage.sprite = equipmentSprite;
        if(descriptionImage.sprite == null){
            descriptionImage.enabled = false;
        }
    }

    // private void ResetStates()
    // {
    //     descriptionName.text = "";
    //     descriptionText.text = "";
    //     descriptionImage.sprite = null;
    // }
}

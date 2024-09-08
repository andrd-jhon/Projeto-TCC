using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject EquipmentMenu;

    public EquipmentField[] allFields;

    public EquipmentField[] swordField;
    public EquipmentField[] bowField;
    public EquipmentField[] shieldField;
    public EquipmentField[] potionField;

    public EquipmentField swordSelected;
    public EquipmentField bowSelected;
    public EquipmentField shieldSelected;
    public EquipmentField potionSelected;

    [SerializeField]
    private MouseFollower mouseFollower;
    
    private bool isActive;

    private void Start() 
    {
        MouseEvents();
        // mouseFollower.Toggle(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isActive){
            Deselect();
            EquipmentMenu.SetActive(false);
            isActive = false;
        }
        else if(Input.GetKeyDown(KeyCode.R) && !isActive){
            EquipmentMenu.SetActive(true);
            isActive = true;
        }
        
    }

    public void Deselect()
    {
        for(int i = 0; i < allFields.Length; i++)
        {
            allFields[i].select.SetActive(false);
            allFields[i].equipmentSelected = false;
            allFields[i].equipmentInformationsUI.descriptionName.text = "";
            allFields[i].equipmentInformationsUI.descriptionText.text = "";
            allFields[i].equipmentInformationsUI.descriptionImage.enabled = false;
        }
    }

    public void VerifyType(EquipmentData equipmentData)
    {
        switch (equipmentData.equipmentCategory)
        {
            case EquipmentCategory.sword:
                // Debug.Log(equipmentData.equipmentCategory);
                SetSword(equipmentData);
                break;
            case EquipmentCategory.bow:
                SetBow(equipmentData);
                break;
            case EquipmentCategory.shield:
                SetShield(equipmentData);
                break;
            case EquipmentCategory.potion:
                SetPotion(equipmentData);
                break;
        }
    }

    public void MouseEvents()
    {
        for(int i = 0; i < allFields.Length; i++)
        {
            allFields[i].OnEquipmentBeginDrag += HandleBeginDrag;
            allFields[i].OnEquipmentDroppedOn += HandleSwap;
            allFields[i].OnEquipmentEndDrag += HandleEndDrag;
            // allFields[i].onpointclick += handlepoint;
        }
    }

    private void HandleBeginDrag(EquipmentField obj)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(obj.equipmentData);
    }

    private void HandleSwap(EquipmentField targetField)
    {
        Debug.Log(targetField.equipmentName);
        if (targetField == null || mouseFollower.currentEquipment == null) return;
        EquipmentField sourceField = FindSourceField(mouseFollower.currentEquipment);
        if (targetField.isFilled)
        {
            EquipmentData tempData = targetField.equipmentData;
            targetField.InsertEquipment(sourceField.equipmentData);
            sourceField.InsertEquipment(tempData);
        }
        else
        {
            targetField.InsertEquipment(sourceField.equipmentData);
            sourceField.RemoveEquipment();
        }
    }
    
    
    private void HandleEndDrag(EquipmentField obj)
    {
        mouseFollower.Toggle(false);
    }

    // private void handlepoint(EquipmentField obj){
    //     Debug.Log(obj.name);
    //     mouseFollower.SetData(obj.equipmentName, obj.equipmentSprite, obj.description, obj.equipmentCategory);
    // }

    private EquipmentField FindSourceField(EquipmentData equipmentData)
    {
        foreach (var field in allFields)
        {
            if (field.equipmentData == equipmentData){
                return field;
            }
        }
    return null;
    }

    private void SetSword(EquipmentData equipmentData)
    {
        if(swordSelected.equipmentName == ""){
            // Debug.Log(equipmentData.equipmentCategory);
            swordSelected.InsertEquipment(equipmentData);
        }
        else{
            for(int i = 0; i < swordField.Length; i++)
            {
                if(swordField[i].isFilled == false)
                {
                    swordField[i].InsertEquipment(equipmentData);
                    return;
                }
            }
        }
    }

    private void SetBow(EquipmentData equipmentData)
    {
        if(bowSelected.equipmentName == null){
            bowSelected.InsertEquipment(equipmentData);
        }
        else{
            for(int i = 0; i < swordField.Length; i++)
            {
                if(bowField[i].isFilled == false)
                {
                    bowField[i].InsertEquipment(equipmentData);
                    return;
                }
            }
        }
    }

    private void SetShield(EquipmentData equipmentData)
    {
        if(shieldSelected.equipmentName == null){
            shieldSelected.InsertEquipment(equipmentData);
        }
        else{
            for(int i = 0; i < shieldField.Length; i++)
            {
                if(shieldField[i].isFilled == false)
                {
                    shieldField[i].InsertEquipment(equipmentData);
                    return;
                }
            }
        }
    }

    private void SetPotion(EquipmentData equipmentData)
    {
        if(potionSelected.equipmentName == null){
            potionSelected.InsertEquipment(equipmentData);
        }
        else{
            for(int i = 0; i < potionField.Length; i++)
            {
                if(potionField[i].isFilled == false)
                {
                    potionField[i].InsertEquipment(equipmentData);
                    return;
                }
            }
        }
    }
}

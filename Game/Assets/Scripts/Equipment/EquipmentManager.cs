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
    
    private bool isActive;

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
            allFields[i].descriptionName.text = "";
            allFields[i].descriptionText.text = "";
            allFields[i].descriptionImage.enabled = false;
        }
    }

    public void VerifyType(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        switch (equipmentCategory)
        {
            case EquipmentCategory.sword:
                SetSword(equipmentName, equipmentSprite, description, equipmentCategory);
                break;
            case EquipmentCategory.bow:
                SetBow(equipmentName, equipmentSprite, description, equipmentCategory);
                break;
            case EquipmentCategory.shield:
                SetShield(equipmentName, equipmentSprite, description, equipmentCategory);
                break;
            case EquipmentCategory.potion:
                SetPotion(equipmentName, equipmentSprite, description, equipmentCategory);
                break;
        }

    }

    private void SetSword(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        if(swordSelected.equipmentName == ""){
            swordSelected.InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        else{
            for(int i = 0; i < swordField.Length; i++)
            {
                if(swordField[i].isFilled == false)
                {
                    swordField[i].InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
                    return;
                }
            }
        }
    }

    private void SetBow(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        if(bowSelected.equipmentName == null){
            bowSelected.InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        else{
            for(int i = 0; i < swordField.Length; i++)
            {
                if(bowField[i].isFilled == false)
                {
                    bowField[i].InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
                    return;
                }
            }
        }
    }

    private void SetShield(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        if(shieldSelected.equipmentName == null){
            shieldSelected.InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        else{
            for(int i = 0; i < shieldField.Length; i++)
            {
                if(shieldField[i].isFilled == false)
                {
                    shieldField[i].InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
                    return;
                }
            }
        }
    }

    private void SetPotion(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        if(potionSelected.equipmentName == null){
            potionSelected.InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        else{
            for(int i = 0; i < potionField.Length; i++)
            {
                if(potionField[i].isFilled == false)
                {
                    potionField[i].InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
                    return;
                }
            }
        }
    }
}

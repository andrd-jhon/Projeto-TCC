using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject EquipmentMenu;
    // public EquipmentField[] equipmentField;

    public SwordSettings swordSettings;
    public BowSettings bowSettings;
    public ShieldSettings shieldSettings;
    public PotionSettings potionSettings;

    private bool isActive;

    // public EquipmentCategory equipmentCategory;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isActive){
            // Deselect();
            EquipmentMenu.SetActive(false);
            isActive = false;
        }
        else if(Input.GetKeyDown(KeyCode.R) && !isActive){
            EquipmentMenu.SetActive(true);
            isActive = true;
        }
        
    }

    public void AddEquipment(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        VerifyType(equipmentName, equipmentSprite, description, equipmentCategory);

        // for(int i = 0; i < equipmentField.Length; i++)
        // {
        //     if(equipmentField[i].isFilled == false)
        //     {
        //         equipmentField[i].InsertEquipment(equipmentName, equipmentSprite, description);
        //         return;
        //     }
        // }
    }

    // public void Deselect()
    // {
        // for(int i = 0; i < equipmentField.Length; i++)
        // {
        //     equipmentField[i].select.SetActive(false);
        //     equipmentField[i].equipmentSelected = false;
        //     equipmentField[i].descriptionName.text = "";
        //     equipmentField[i].descriptionText.text = "";
        //     equipmentField[i].descriptionImage.sprite = null;
        //     equipmentField[i].select.SetActive(false);
        // }
    // }

    private void VerifyType(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory)
    {
        if(equipmentCategory == EquipmentCategory.sword){
            swordSettings.InsertSword(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        if(equipmentCategory == EquipmentCategory.bow){
            bowSettings.InsertBow(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        if(equipmentCategory == EquipmentCategory.shield){
            shieldSettings.InsertShield(equipmentName, equipmentSprite, description, equipmentCategory);
        }
        if(equipmentCategory == EquipmentCategory.potion){
            potionSettings.InsertPotion(equipmentName, equipmentSprite, description, equipmentCategory);
        }
    }
}

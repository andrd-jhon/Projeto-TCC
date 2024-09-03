using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject EquipmentMenu;
    public EquipmentField[] equipmentField;

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

    public void AddEquipment(string equipmentName, Sprite equipmentSprite, string description)
    {
        for(int i = 0; i < equipmentField.Length; i++)
        {
            if(equipmentField[i].isFilled == false)
            {
                equipmentField[i].InsertEquipment(equipmentName, equipmentSprite, description);
                return;
            }
        }
    }

    public void Deselect()
    {
        for(int i = 0; i < equipmentField.Length; i++)
        {
            equipmentField[i].select.SetActive(false);
            equipmentField[i].equipmentSelected = false;
            equipmentField[i].descriptionName.text = "";
            equipmentField[i].descriptionText.text = "";
            equipmentField[i].descriptionImage.sprite = null;
            equipmentField[i].select.SetActive(false);
        }
    }
}
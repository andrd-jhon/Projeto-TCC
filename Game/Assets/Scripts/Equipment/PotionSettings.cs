using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSettings : MonoBehaviour
{
    public EquipmentField[] equipmentField;

    public void InsertPotion(string equipmentName, Sprite equipmentSprite, string description, EquipmentCategory equipmentCategory){
        for(int i = 0; i < equipmentField.Length; i++)
        {
            if(equipmentField[i].isFilled == false)
            {
                equipmentField[i].InsertEquipment(equipmentName, equipmentSprite, description, equipmentCategory);
                return;
            }
        }
    }
}

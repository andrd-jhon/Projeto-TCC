using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipmentInformationsUI : MonoBehaviour
{
    public Image descriptionImage;
    public TMP_Text descriptionName;
    public TMP_Text descriptionText;

    public void SetInformations(EquipmentData equipmentData)
    {
        descriptionImage.enabled = true;
        // Debug.Log(equipmentData.equipmentName + " pao");
        descriptionName.text = equipmentData.equipmentName;
        descriptionText.text = equipmentData.description;
        descriptionImage.sprite = equipmentData.image;
        if(descriptionImage.sprite == null){
            descriptionImage.enabled = false;
        }
    }
}

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

    public void SetInformations(string equipmentName, Sprite equipmentSprite, string description)
    {
        descriptionImage.enabled = true;
        descriptionName.text = equipmentName;
        descriptionText.text = description;
        descriptionImage.sprite = equipmentSprite;
        if(descriptionImage.sprite == null){
            descriptionImage.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectedEquipments : MonoBehaviour
{
    public EquipmentField swordSelected, bowSelected, shieldSelected, potionSelected;

    [SerializeField]
    private Image swordImage, bowImage, shieldImage, potionImage;

    public void SetSelected(EquipmentData equipmentData)
    {  
        switch (equipmentData.equipmentCategory)
        {
            case EquipmentCategory.sword:
                swordImage.enabled = true;
                swordImage.sprite = equipmentData.imageSelected;
                break;
            case EquipmentCategory.bow:
                bowImage.enabled = true;
                bowImage.sprite = equipmentData.imageSelected;
                break;
            case EquipmentCategory.shield:
                shieldImage.enabled = true;
                shieldImage.sprite = equipmentData.imageSelected;
                break;
            case EquipmentCategory.potion:
                potionImage.enabled = true;
                potionImage.sprite = equipmentData.imageSelected;
                break;
        }

        // if(swordSprite == null) swordImage.enabled = false;
        // if(bowSprite == null) swordImage.enabled = false;
        // if(shieldSprite == null) swordImage.enabled = false;
        // if(potionSprite == null) swordImage.enabled = false;

        // swordImage.sprite = swordSprite;
        // bowImage.sprite = bowSprite;
        // shieldImage.sprite = shieldSprite;
        // potionImage.sprite = potionSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectedEquipments : MonoBehaviour
{
    EquipmentManager equipmentManager;

    public Coroutine changeWeaponCoroutine;

    public EquipmentField swordSelected, bowSelected, shieldSelected, potionSelected;

    [SerializeField]
    public Image swordImage, bowImage, shieldImage, potionImage;


    private void Start()
    {
        equipmentManager = FindFirstObjectByType<EquipmentManager>();
        
    }

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
    }

    public IEnumerator ChangeWeapon(){
        while(equipmentManager.isActive){
            ShowSword();
            yield return new WaitForSeconds(2f);
            ShowBow();
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }

    private void ShowBow(){
        bowImage.enabled = true;
        swordImage.enabled = false;
    }
    
    private void ShowSword(){
        swordImage.enabled = true;
        bowImage.enabled = false;
    }

    public void StartChangeWeaponCoroutine(){
        if (changeWeaponCoroutine == null){
            changeWeaponCoroutine = StartCoroutine(ChangeWeapon());
        }
    }

    public void StopChangeWeaponCoroutine(){
        if (changeWeaponCoroutine != null){
            StopCoroutine(changeWeaponCoroutine);
            changeWeaponCoroutine = null;
        }
    }

    public void Selected(EquipmentData equipmentData)
    {

    }
}

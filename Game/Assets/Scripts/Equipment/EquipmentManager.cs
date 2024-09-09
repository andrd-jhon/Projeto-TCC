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

    // public EquipmentField swordSelected;
    // public EquipmentField bowSelected;
    // public EquipmentField shieldSelected;
    // public EquipmentField potionSelected;

    [SerializeField]
    private SelectedEquipments selectedEquipments;

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
        if(targetField == null || mouseFollower.currentEquipment == null || targetField.equipmentCategory != mouseFollower.currentEquipment.equipmentCategory) return; //verifica se é nulo, se a categoria é correta e se o equipamento selecionado está preenchido
        EquipmentField sourceField = FindSourceField(mouseFollower.currentEquipment); //variável referente ao campo origem
        if(targetField.isFilled) //verifica se há item no campo destino
        {
            EquipmentData tempData = targetField.equipmentData; //cria variável temporária com dados do campo destino
            targetField.InsertEquipment(sourceField.equipmentData); //campo destino recebe dados do seguidor do mouse
            sourceField.InsertEquipment(tempData); //campo orifem recebe dados do destino
        }
        else if(sourceField == selectedEquipments.swordSelected || sourceField == selectedEquipments.bowSelected || sourceField == selectedEquipments.shieldSelected || sourceField == selectedEquipments.potionSelected) return;
        else // se não houver o campo apenas recebe os dados do original
        {
            targetField.InsertEquipment(sourceField.equipmentData); //insere o capo origem no campo destino
            sourceField.RemoveEquipment();  //remove os dados do campo origem
        }
    }
    
    
    private void HandleEndDrag(EquipmentField obj)
    {
        // shieldSelected.InsertEquipment(mouseFollower.equipmentField.equipmentData); FUNCIONA
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
        if(selectedEquipments.swordSelected.equipmentName == ""){
            // Debug.Log(equipmentData.equipmentCategory);
            selectedEquipments.swordSelected.InsertEquipment(equipmentData);
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
        if(selectedEquipments.bowSelected.equipmentName == ""){
            selectedEquipments.bowSelected.InsertEquipment(equipmentData);
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
        if(selectedEquipments.shieldSelected.equipmentName == ""){
            selectedEquipments.shieldSelected.InsertEquipment(equipmentData);
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
        if(selectedEquipments.potionSelected.equipmentName == ""){
            selectedEquipments.potionSelected.InsertEquipment(equipmentData);
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

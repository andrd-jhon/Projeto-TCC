using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{


    public GameObject EquipmentMenu;

    public EquipmentField[] allFields, swordField, bowField, shieldField, potionField;

    [SerializeField]
    private SelectedEquipments selectedEquipments;

    [SerializeField]
    private MouseFollower mouseFollower;
    
    public bool isActive;

    private void Start() 
    {
        MouseEvents();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isActive){
            Deselect();
            PlayerController.state = PLAYER.FREE;
            EquipmentMenu.SetActive(false);
            isActive = false;
            selectedEquipments.StopChangeWeaponCoroutine();
        }
        else if(Input.GetKeyDown(KeyCode.R) && !isActive){
            PlayerController.state = PLAYER.INTERACT;
            EquipmentMenu.SetActive(true);
            isActive = true;
            if (selectedEquipments.swordSelected.equipmentData != null && selectedEquipments.bowSelected.equipmentData != null){
            selectedEquipments.StartChangeWeaponCoroutine();
            }
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

            if(sourceField == selectedEquipments.swordSelected || sourceField == selectedEquipments.bowSelected || sourceField == selectedEquipments.shieldSelected || sourceField == selectedEquipments.potionSelected){
                selectedEquipments.SetSelected(tempData);
            }
            if(targetField == selectedEquipments.swordSelected || targetField == selectedEquipments.bowSelected || targetField == selectedEquipments.shieldSelected || targetField == selectedEquipments.potionSelected){
                selectedEquipments.SetSelected(sourceField.equipmentData);
            }
            sourceField.InsertEquipment(tempData);

        }//campo origem recebe dados do destino
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


    public void VerifyType(EquipmentData equipmentData)
    {
        switch (equipmentData.equipmentCategory)
        {
            case EquipmentCategory.sword:
                SetEquipment(equipmentData, selectedEquipments.swordSelected, swordField);
                break;
            case EquipmentCategory.bow:
                SetEquipment(equipmentData, selectedEquipments.bowSelected, bowField);
                break;
            case EquipmentCategory.shield:
                SetEquipment(equipmentData, selectedEquipments.shieldSelected, shieldField);
                break;
            case EquipmentCategory.potion:
                SetEquipment(equipmentData, selectedEquipments.potionSelected, potionField);
                break;
        }
    }

    private void SetEquipment(EquipmentData equipmentData, EquipmentField selectEquipment, EquipmentField[] deselectEquipment) //Fazer
    {

        if(selectEquipment.equipmentName == ""){
            selectEquipment.InsertEquipment(equipmentData);
            selectedEquipments.SetSelected(equipmentData);
        }
        else{
            for(int i = 0; i < deselectEquipment.Length; i++)
            {
                if(deselectEquipment[i].isFilled == false)
                {
                    deselectEquipment[i].InsertEquipment(equipmentData);
                    return;
                }
            }
        }
    }
}

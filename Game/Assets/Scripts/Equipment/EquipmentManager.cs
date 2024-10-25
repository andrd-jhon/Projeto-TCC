using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField] private List<EquipmentData> allEquipments;
    public GameObject EquipmentMenu;

    public EquipmentField[] allFields, swordField, bowField, shieldField, potionField;

    [SerializeField]
    private SelectedEquipments selectedEquipments;

    [SerializeField]
    private MouseFollower mouseFollower;
    
    public bool isActive;

    public List<EquipmentData> equipmentsReserved = new List<EquipmentData>();
    public List<EquipmentData> equipmentsSelected = new List<EquipmentData>();
    public List<string> equipmentsReservedName = new List<string>();
    public List<string> equipmentsSelectedName = new List<string>();

    private void Start() 
    {
        MouseEvents();
        LoadEquipments();
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
        EquipmentField sourceField = FindSourceField(mouseFollower.currentEquipment); //CAMPO ORIGEM
        if(targetField.isFilled) //verifica se há item no campo destino
        {
            EquipmentData tempData = targetField.equipmentData; //CAMPO DESTINO (TEMPORÁRIO)
            targetField.InsertEquipment(sourceField.equipmentData); //CAMPO DESTINO RECEBE DADOS DO CAMPO ORIGEM

            //SE CAMPO ORIGEM FOR UM CAMPO DOS EQUIPAMENTOS SELECIONADOS
            if(sourceField == selectedEquipments.swordSelected || sourceField == selectedEquipments.bowSelected || sourceField == selectedEquipments.shieldSelected || sourceField == selectedEquipments.potionSelected){
                selectedEquipments.SetSelected(tempData);
                equipmentsSelected.Remove(sourceField.equipmentData);
                equipmentsSelected.Add(tempData);
                equipmentsReserved.Remove(tempData);
                equipmentsReserved.Add(sourceField.equipmentData);

                equipmentsSelectedName.Remove(sourceField.equipmentData.equipmentName);
                equipmentsSelectedName.Add(tempData.equipmentName);
                equipmentsReservedName.Remove(tempData.equipmentName);
                equipmentsReservedName.Add(sourceField.equipmentData.equipmentName);

                EditListEquipment();
            }

            //SE CAMPO DESTINO FOR UM CAMPO DOS EQUIPAMENTOS SELECIONADOS
            if(targetField == selectedEquipments.swordSelected || targetField == selectedEquipments.bowSelected || targetField == selectedEquipments.shieldSelected || targetField == selectedEquipments.potionSelected){
                selectedEquipments.SetSelected(sourceField.equipmentData);
                equipmentsReserved.Remove(sourceField.equipmentData);
                equipmentsReserved.Add(tempData);
                equipmentsSelected.Remove(tempData);
                equipmentsSelected.Add(sourceField.equipmentData);

                equipmentsReservedName.Remove(sourceField.equipmentData.equipmentName);
                equipmentsReservedName.Add(tempData.equipmentName);
                equipmentsSelectedName.Remove(tempData.equipmentName);
                equipmentsSelectedName.Add(sourceField.equipmentData.equipmentName);
                
                EditListEquipment();
            }
            sourceField.InsertEquipment(tempData); //CAMPO ORIGEM RECEBE DADOS DO CAMPO DESTINO

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
        mouseFollower.Toggle(false);
    }

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
            InsertInSelected(equipmentData, selectEquipment);
        }
        else{
            InsertInReserved(equipmentData, deselectEquipment);
        }
    }

    public void EditListEquipment()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        if(data != null)
        {
            data.reservedEquipments = equipmentsReservedName;
            data.selectedEquipments = equipmentsSelectedName;

            // data.reservedEquipments.AddRange(equipmentsReservedName);
            // data.selectedEquipments.AddRange(equipmentsSelectedName);
            GameManager.instance.SaveGame(data);
        }
    }

    private void InsertInSelected(EquipmentData equipmentData, EquipmentField selectEquipment)
    {
        selectEquipment.InsertEquipment(equipmentData);
        selectedEquipments.SetSelected(equipmentData);
        equipmentsSelected.Add(equipmentData);
        equipmentsSelectedName.Add(equipmentData.equipmentName);
        EditListEquipment();
    }

    private void InsertInReserved(EquipmentData equipmentData, EquipmentField[] deselectEquipment)
    {
        for(int i = 0; i < deselectEquipment.Length; i++)
        {
            if(deselectEquipment[i].isFilled == false)
            {
                deselectEquipment[i].InsertEquipment(equipmentData);
                equipmentsReserved.Add(equipmentData);
                equipmentsReservedName.Add(equipmentData.equipmentName);
                EditListEquipment();
                return;
            }
        }
    }

    private void InsertEquipmentBasedOnCategory(EquipmentData equipmentData, string name, List<string> equipmentList, Action<EquipmentData, EquipmentField[]> insertMethod)
    {
        if (equipmentList.Contains(name))
        {
            switch (equipmentData.equipmentCategory)
            {
                case EquipmentCategory.sword:
                    insertMethod(equipmentData, swordField);
                    break;
                case EquipmentCategory.bow:
                    insertMethod(equipmentData, bowField);
                    break;
                case EquipmentCategory.shield:
                    insertMethod(equipmentData, shieldField);
                    break;
                case EquipmentCategory.potion:
                    insertMethod(equipmentData, potionField);
                    break;
            }
            Debug.Log("O NOME DESTE EQUIPAMENTO É " + equipmentData.equipmentName);
        }
    }

    private void LoadEquipments()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        if (data.selectedEquipments != null)
        {
            foreach (EquipmentData _equipment in allEquipments)
            {
                InsertEquipmentBasedOnCategory(_equipment, _equipment.equipmentName, data.selectedEquipments, (equipment, equipmentArray) => InsertInSelected(equipment, selectedEquipments.GetFieldByCategory(equipment.equipmentCategory)));
                InsertEquipmentBasedOnCategory(_equipment, _equipment.equipmentName, data.reservedEquipments, InsertInReserved);
            }
        }
    }
}
//     private void LoadEquipments()
//     {
//         GameManager.GameData data = GameManager.instance.LoadGame();
//         if(data.selectedEquipments != null)
//         {
//             foreach (EquipmentData _equipment in allEquipments)
//             {
//                 foreach (string nameEquipmentSelected in data.selectedEquipments)
//                 {
//                     if(_equipment.equipmentName == nameEquipmentSelected)
//                     {
//                         switch (_equipment.equipmentCategory)
//                         {
//                             case EquipmentCategory.sword:
//                                 InsertInSelected(_equipment, selectedEquipments.swordSelected);
//                                 break;
//                             case EquipmentCategory.bow:
//                                 InsertInSelected(_equipment, selectedEquipments.bowSelected);
//                                 break;
//                             case EquipmentCategory.shield:
//                                 InsertInSelected(_equipment, selectedEquipments.shieldSelected);
//                                 break;
//                             case EquipmentCategory.potion:
//                                 InsertInSelected(_equipment, selectedEquipments.potionSelected);
//                                 break;
//                         }
//                         Debug.Log("O NOME DESTE EQUIPAMENTO SELECIONADO É "+ _equipment.equipmentName);
//                     }
//                 }
//                 foreach (string nameEquipmentReserved in data.reservedEquipments)
//                 {
//                     if(_equipment.equipmentName == nameEquipmentReserved)
//                     {
//                         switch (_equipment.equipmentCategory)
//                         {
//                             case EquipmentCategory.sword:
//                                 InsertInReserved(_equipment, swordField);
//                                 break;
//                             case EquipmentCategory.bow:
//                                 InsertInReserved(_equipment, bowField);
//                                 break;
//                             case EquipmentCategory.shield:
//                                 InsertInReserved(_equipment, shieldField);
//                                 break;
//                             case EquipmentCategory.potion:
//                                 InsertInReserved(_equipment, potionField);
//                                 break;
//                         }
//                         Debug.Log("O NOME DESTE EQUIPAMENTO RESERVADO É "+ _equipment.equipmentName);
//                     }
//                 }
//             }
//         }
//     }
// }

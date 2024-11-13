using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformManager : MonoBehaviour
{
    [SerializeField] private List<EquipmentData> allEquipments;

    private void Awake()
    {
        CarrySelectedEquipments();
    }

    private void CarrySelectedEquipments()
    {
        GameObject playerPlat = GameObject.Find("Player");
        GameManager.GameData data = GameManager.instance.LoadGame();
        if(data.selectedEquipments != null)
        {
            foreach(string equipmentName in data.selectedEquipments)
            {
                EquipmentData currentEquipment = TakeEquipment(equipmentName);
                if(currentEquipment.equipmentGameObject != null)
                {
                    Instantiate(currentEquipment.equipmentGameObject, playerPlat.transform);
                }
            }
        }
    }

    private EquipmentData TakeEquipment (string equipmentName)
    {
        foreach (EquipmentData equipment in allEquipments)
        {
            if(equipment.equipmentName == equipmentName)
            {
                return equipment;
            }
        }
        return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "EquipmentData", menuName = "ScriptableObjects/EquipmentData", order = 1)]
    public class EquipmentData : ScriptableObject {
    
    public string equipmentName;
    public Sprite image;

    [TextArea]
    public string description;
    
    public EquipmentCategory equipmentCategory = new EquipmentCategory();

}
public enum EquipmentCategory{
        sword,
        bow,
        shield,
        potion,
};
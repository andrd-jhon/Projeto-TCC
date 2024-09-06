using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public EquipmentData equipmentData;

    private EquipmentManager equipmentManager;

    public EquipmentCategory equipmentCategory;
    
    void Start()
    {
        equipmentManager = GameObject.Find("EquipmentSystem").GetComponent<EquipmentManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            equipmentManager.VerifyType(equipmentData.equipmentName, equipmentData.image, equipmentData.description, equipmentData.equipmentCategory);
            Destroy(gameObject);
        }
    }
}

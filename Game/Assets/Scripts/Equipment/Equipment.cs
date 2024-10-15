using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Equipment : MonoBehaviour, IInteractable
{
    public UnityEvent getEquipemnt;

    public EquipmentData equipmentData;

    // public Action getEquipemnt;

    private EquipmentManager equipmentManager;

    // public EquipmentCategory equipmentCategory;
    
    void Start()
    {
        equipmentManager = GameObject.Find("EquipmentSystem").GetComponent<EquipmentManager>();
    }

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Player"){
    //         equipmentManager.VerifyType(equipmentData);
    //         Destroy(gameObject);
    //         // Debug.Log(equipmentData.equipmentName);
    //     }
    // }

    public void Interact(){
        PickUpEquipment();
    }

    public void PickUpEquipment(){
        equipmentManager.VerifyType(equipmentData);
        getEquipemnt?.Invoke();
        Destroy(gameObject);
    }
}

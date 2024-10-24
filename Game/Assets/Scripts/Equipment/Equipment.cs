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
    
    void Awake()
    {
        equipmentManager = GameObject.Find("EquipmentSystem").GetComponent<EquipmentManager>();
    }

    private void Start() {
        GameManager.GameData data = GameManager.instance.LoadGame();
        if (data.destroyedObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);  // Destrói se o objeto foi marcado como destruído
        }
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
        // SaveEquipmentState();
        DestroyPermanently();
        Destroy(gameObject);
    }

    // public void SaveEquipmentState()
    // {
    //     GameManager.GameData data = new GameManager.GameData()
    //     {
    //         playerPosition = new Vector2(UnityEngine.Random.Range(0,9), UnityEngine.Random.Range(0,9))
    //     };
    //     saveManager.SaveGame(data);
    // }

    public void DestroyPermanently()
    {
        GameManager.GameData data = GameManager.instance.LoadGame();
        if (data != null)
        {
            data.destroyedObjects.Add(gameObject.name);
            GameManager.instance.SaveGame(data);
        }
    }
}

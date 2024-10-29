using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Equipment : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject outlineOBJ;
    private SpriteRenderer spriteEquipment;
    public SpriteRenderer outline;

    public UnityEvent getEquipemnt;

    public EquipmentData equipmentData;

    // public Action getEquipemnt;

    private EquipmentManager equipmentManager;

    private void Awake()
    {
        spriteEquipment = GetComponent<SpriteRenderer>();
        outline = outlineOBJ.GetComponent<SpriteRenderer>();
        outline.sprite = spriteEquipment.sprite;
    }

    private void Start() {
        equipmentManager = FindFirstObjectByType<EquipmentManager>();
        
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
        DestroyPermanently();
        Destroy(gameObject);
    }

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
